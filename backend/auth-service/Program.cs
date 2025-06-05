using auth_servise.Core.Application.Common.Mappings;
using auth_servise.Core.Application.Interfaces.Auth;
using auth_servise.Core.Application.Interfaces.NotificationService;
using auth_servise.Core.Application.Interfaces.RabbitMq;
using auth_servise.Core.Application.Interfaces.Repositories;
using auth_servise.Infrastructure.Jwt;
using auth_servise.Infrastructure.Options;
using auth_servise.Infrastructure.PasswordHasher;
using auth_servise.Infrastructure.Persistence;
using auth_servise.Infrastructure.RabbitMq;
using auth_servise.Infrastructure.UsedServices.Connectors;
using auth_servise.Presentation.HostedServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using static auth_servise.Presentation.HostedServices.ServicesOptions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var applicationTag = configuration.GetSection("ApplicationTag").Value;

services.Configure<ServiceEnvironmentOptions>(configuration.GetSection(nameof(ServiceEnvironmentOptions)));

services.Configure<RabbitMqOptions>(configuration.GetSection(nameof(RabbitMqOptions)));

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.Configure<ServicesOptions>(configuration.GetSection(nameof(ServicesOptions)));
services.Configure<DeleteOldRegistrationAttemptsServiceOptions>(configuration
    .GetSection(nameof(ServicesOptions))
    .GetSection(nameof(DeleteOldRegistrationAttemptsServiceOptions)));
services.Configure<Urls>(configuration
    .GetSection(nameof(ServicesOptions))
    .GetSection(nameof(Urls)));


services.Configure<AdminSettingsOptions>(configuration.GetSection(nameof(AdminSettingsOptions)));

//Serilog
services.AddSerilog(new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/auth-service-logs.txt", rollingInterval: RollingInterval.Month)
    .CreateLogger());

services.AddControllers();
services.AddAutoMapper(config =>
{
    //config.AddProfile(new AssemblyMappingProfile(typeof(Program).Assembly));
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IAuthServiseDbContext).Assembly));
});

// HostedServises
services.AddHostedService<DeleteOldRegistrationAttemptsService>();

// RabbitMq
var rabbitMqOptions = configuration.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>();

services.AddSingleton<IRabbitMqService, RabbitMqService>();

var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["mini-cookie"];
                return Task.CompletedTask;
            }
        };
    });

services.AddAuthorization();

// Persistance
var connectionString = configuration.GetConnectionString("Database");
services.AddDbContext<AuthServiseDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
services.AddScoped<IAuthServiseDbContext>(provider =>
                provider.GetService<AuthServiseDbContext>());

// Application
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


// Hasher
services.AddTransient<IHasher, Hasher>();

// JwtAuth
services.AddTransient<IJwtProvider, JwtProvider>();

// Notificate
services.AddTransient<ISendEmailInfoToNotificationService, NotificationServiceConnector>();
services.AddTransient<IManageNotificationUserSettings, NotificationServiceConnector>();
services.AddTransient<ICheckEmailNotificationByRA, NotificationServiceConnector>();

var webClientCorsOrigin = configuration.GetSection("WebClient").GetSection("CorsOrigin").Value;

services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins(webClientCorsOrigin!);
        policy.AllowCredentials();
    });
});

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.ConfigureSwaggerGen(options =>
{
    options.SwaggerDoc(applicationTag, new OpenApiInfo
    {
        Version = applicationTag,
        Title = "Auth-Service API",
        Description = "An ASP.NET Core Web API for managing users and their access rights.",
        TermsOfService = new Uri("https://github.com/Vinograd2702/MyAssistant"),
        Contact = new OpenApiContact
        {
            Name = "my mail \"grublyakvlad@yandex.ru\"",
            Email = "grublyakvlad@yandex.ru"
        },
        License = new OpenApiLicense
        {
            Name = "Project License",
            Url = new Uri("https://github.com/Vinograd2702/MyAssistant/blob/master/LICENSE")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/" + applicationTag + "/swagger.json", applicationTag);
    options.RoutePrefix = "swagger";
});

// Init BD
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<AuthServiseDbContext>();
        var adminOptions = configuration.GetSection(nameof(AdminSettingsOptions)).Get<AdminSettingsOptions>();
        var passwordHasher = app.Services.GetRequiredService<IHasher>();
        DbInitializer.Initialize(context, adminOptions, passwordHasher);
    }
    catch (Exception ex)
    {

    }
}

// Init RabbitMq
try
{
    var rabbitMqService = app.Services.GetRequiredService<IRabbitMqService>();
    await rabbitMqService.CreateConnection();
}
catch(Exception ex)
{

}


app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});


app.UseAuthentication();
app.UseAuthorization();
app.Run();
