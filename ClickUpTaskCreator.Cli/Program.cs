using System.CommandLine.Builder;
using System.CommandLine;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ClickUpTaskCreator.Cli.Commands;
using System.CommandLine.Parsing;
using ClickUpTaskCreator.Cli.Options;
using Microsoft.Extensions.Logging;
using ClickUpTaskCreator.Cli.Helpers;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
Console.WriteLine(environmentName);
var hostBuilder = new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<CreateTaskCommandHandler>();
        services.AddTransient<CreateTaskCommand>();
        services.AddTransient<ConfigureClientCommand>();
        services.AddTransient<ConfigureClientCommandHandler>();
        services.AddSingleton<OptionCollection>();
        
        services.AddHttpClient();
        if (OperatingSystem.IsWindows())
            services.AddLogging(cfg => cfg.AddEventLog());
        var configuration = RegistryHelpers.GetValueFromRegitry<ClickUpConfiguration>(nameof(ClickUpConfiguration));
        if(configuration==null)
            configuration = new ClickUpConfiguration();
        services.AddSingleton(configuration);
        
    });
var app = hostBuilder.Build();
using var scope = app.Services.CreateScope();
var configureCommand = scope.ServiceProvider.GetRequiredService<ConfigureClientCommand>();
var createCommand = scope.ServiceProvider.GetRequiredService<CreateTaskCommand>();
var rootCommand = new RootCommand
{
    createCommand,
    configureCommand,
};

var parser = new CommandLineBuilder(rootCommand)
    .UseDefaults();
await parser.Build().InvokeAsync(args);


     

