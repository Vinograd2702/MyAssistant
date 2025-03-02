using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Mappings;
using sports_service.Core.Application.Interfaces.Auth;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Infrastructure.Jwt;
using sports_service.Infrastructure.Persistence;
using sports_service.Presentation.HostedServices;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers();

services.AddAutoMapper(config =>
{
    //config.AddProfile(new AssemblyMappingProfile(typeof(Program).Assembly));
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ISportServiseDbContext).Assembly));
});

// HostedServises

services.AddHostedService<CheckNeedNotifyForUsersAbautWorkoutService>();

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

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

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
