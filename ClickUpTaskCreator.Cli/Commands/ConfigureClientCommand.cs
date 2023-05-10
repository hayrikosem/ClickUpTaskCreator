using ClickUpTaskCreator.Cli.Helpers;
using ClickUpTaskCreator.Cli.Options;
using Microsoft.Win32;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;

namespace ClickUpTaskCreator.Cli.Commands;

public class ConfigureClientCommand : Command
{
    const string name = "configure";

    public ConfigureClientCommand(ConfigureClientCommandHandler handler, OptionCollection options) : base(name)
    {
        Handler = handler;
        AddOption(new Option<string>("--apiKey", "ApiKey for clickup")
        {
            IsRequired = true,
        });
        AddOption(new Option<string>("--apiUrl", "Api url for ClickUp Api."));
        AddOption(new Option<string>("--listId", "ListId for the tasks to be added.")
        {
            IsRequired = true
        });
        options.AddRange(Options);
    }
}
public class ConfigureClientCommandHandler:ICommandHandler
{
    private readonly OptionCollection _options;
    private const string registryKey = @"Software\CHKSolutions\ctask";
    public ConfigureClientCommandHandler(OptionCollection options)
    {
        _options = options;
    }

    int ICommandHandler.Invoke(InvocationContext context)
    {
        throw new NotImplementedException();
    }

    Task<int> ICommandHandler.InvokeAsync(InvocationContext context)
    {
        string encryptedValue = GetEncryptedConfiguration(context);
        RegistryHelpers.AddValueToRegistery(nameof(ClickUpConfiguration), encryptedValue);
        return Task.FromResult(0);
    }

    private string GetEncryptedConfiguration(InvocationContext context)
    {
        var apiKey = context.GetOptionValue<string>(_options, "apiKey");
        var url = context.GetOptionValue<string>(_options, "apiUrl") ?? "https://api.clickup.com";
        var listId = context.GetOptionValue<string>(_options, "listId");
        var configuration = new ClickUpConfiguration
        {
            ApiKey = apiKey,
            ApiUrl = url,
            ListId = listId,
        };
        return JsonSerializer.Serialize(configuration);
    }
}