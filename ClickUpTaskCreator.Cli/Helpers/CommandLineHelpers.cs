using ClickUpTaskCreator.Cli.Options;
using System.CommandLine.Invocation;

namespace ClickUpTaskCreator.Cli.Helpers;

public static class CommandLineHelpers
{
    public static T GetOptionValue<T>(this InvocationContext context,OptionCollection collection, string optionName)
    {
        return (T)context.ParseResult.GetValueForOption(collection.First(x => x.Name == optionName));
    }
}