using System.Runtime.Serialization;

namespace ClickUpTaskCreator.Cli.Exceptions;

public class ConfigurationNotFoundException : Exception
{
    public string MissiongPropertyName { get; set; }
    public ConfigurationNotFoundException(string missiongPropertyName)
    {
        MissiongPropertyName = missiongPropertyName;
    }

    public ConfigurationNotFoundException(string? message, string missiongPropertyName) : base(message)
    {
        MissiongPropertyName = missiongPropertyName;
    }

    public ConfigurationNotFoundException(string? message, Exception? innerException, string missiongPropertyName) : base(message, innerException)
    {
        MissiongPropertyName = missiongPropertyName;
    }

    protected ConfigurationNotFoundException(SerializationInfo info, StreamingContext context, string missiongPropertyName) : base(info, context)
    {
        MissiongPropertyName = missiongPropertyName;
    }
}