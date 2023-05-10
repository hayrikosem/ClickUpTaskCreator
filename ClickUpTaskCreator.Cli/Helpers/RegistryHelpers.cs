using ClickUpTaskCreator.Cli.Options;
using Microsoft.Win32;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace ClickUpTaskCreator.Cli.Helpers;

public static class RegistryHelpers
{
    const string _registryKey= @"Software\CHKSolutions\ctask";
    public static void AddValueToRegistery(string key, string value)
    {
        if (OperatingSystem.IsWindows() == false)
            throw new NotSupportedException();
        using var registryKey = Registry.CurrentUser.CreateSubKey(_registryKey);
        registryKey.SetValue(key, value.Encrypt());
    }
    public static T? GetValueFromRegitry<T>(string key) where T : class
    {
        if (OperatingSystem.IsWindows() == false)
            throw new NotSupportedException();
        using var registryKey = Registry.CurrentUser.OpenSubKey(_registryKey);
        var value = registryKey.GetValue(key)?.ToString();
        if (value == null)
            return null;

        return JsonSerializer.Deserialize<T>(value.Decrypt());

    }
}