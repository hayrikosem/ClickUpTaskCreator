namespace ClickUpTaskCreator.Cli.Helpers;

public static class DateTimeHelpers
{
    public static long ToEpochMilliseconds(this DateTime dateTime)
    {
        return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }
}