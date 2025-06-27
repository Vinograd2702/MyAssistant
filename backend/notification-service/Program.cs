using notification_service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using notification_service.Core.Application.Interfaces.Repositories;
using notification_service.Infrastructure.RabbitMq;
using Serilog;
using System.Reflection;
using notification_service.Core.Application.Interfaces.RabbitMq;
using notification_service.Infrastructure.Options;
using notification_service.Presentation.HostedServices;
using static notification_service.Presentation.HostedServices.ServicesOptions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var applicationTag = configuration.GetSection("ApplicationTag").Value;

services.Configure<ServiceEnvironmentOptions>(configuration.GetSection(nameof(ServiceEnvironmentOptions)));

services.Configure<RabbitMqOptions>(configuration.GetSection(nameof(RabbitMqOptions)));

services.Configure<ServicesOptions>(configuration.GetSection(nameof(ServicesOptions)));
services.Configure<SendCheckEmailNotificationServiceOptions>(configuration
    .GetSection(nameof(ServicesOptions))
    .GetSection(nameof(SendCheckEmailNotificationServiceOptions)));

//Serilog
services.AddSerilog(new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/notification-service-logs.txt", rollingInterval: RollingInterval.Month)
    .CreateLogger());


// Persistance
var connectionString = configuration.GetConnectionString("Database");
services.AddDbContext<NotificationServiseDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
services.AddScoped<INotificationServiseDbContext>(provider =>
                provider.GetService<NotificationServiseDbContext>());

// Application
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// HostedServises
services.AddHostedService<SendCheckEmailNotificationService>();

//RabbitMq
var rabbitMqOptions = configuration.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>();

services.AddSingleton<IRabbitMqService, RabbitMqService>();

//RabbitMq ResiveMessageService
services.AddScoped<IResiveMessageService, ResiveMessageService>();


var app = builder.Build();

// Init BD
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<NotificationServiseDbContext>();
        DbInitializer.Initialize(context);
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
catch (Exception ex)
{

}

app.Run();
