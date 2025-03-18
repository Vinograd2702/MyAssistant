using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using sports_service.Core.Application.Common.Mappings;
using sports_service.Core.Application.Interfaces.Auth;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Infrastructure.Jwt;
using sports_service.Infrastructure.Persistence;
using sports_service.Presentation.HostedServices;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var applicationTag = configuration.GetSection("ApplicationTag").Value;

//Serilog
services.AddSerilog(new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/sports-service-logs.txt", rollingInterval: RollingInterval.Month)
    .CreateLogger());

services.AddControllers();

services.AddAutoMapper(config =>
{
    //config.AddProfile(new AssemblyMappingProfile(typeof(Program).Assembly));
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ISportServiseDbContext).Assembly));
});

// HostedServises
services.AddHostedService<CheckNeedNotifyForUsersAbautWorkoutService>();

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
services.AddDbContext<SportServiseDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
services.AddScoped<ISportServiseDbContext>(provider =>
                provider.GetService<SportServiseDbContext>());

// Application
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// JwtAuth
services.AddTransient<IJwtProvider, JwtProvider>();

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.ConfigureSwaggerGen(options =>
{
    options.SwaggerDoc(applicationTag, new OpenApiInfo
    {
        Version = applicationTag,
        Title = "Sports-Service API",
        Description = "An ASP.NET Core Web API for managing users sports activity.",
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
    var serviceProvaider = scope.ServiceProvider;
    try
    {
        var context = serviceProvaider.GetRequiredService<SportServiseDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {

    }
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
