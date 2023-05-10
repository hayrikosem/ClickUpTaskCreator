using ClickUpTaskCreator.Cli.Helpers;
using System.Text.Json.Serialization;

namespace ClickUpTaskCreator.Cli.Dtos;

public class ClickUpTask
{
    public ClickUpTask(string name, string description,string customerName)
    {
        this.name = name;
        this.description = description;
        custom_fields = new List<CustomField>
        {
            new CustomField
            {
                id ="cbe32a9e-914a-47a7-a25a-29874ed6918c",
                value = customerName
            }
        };
        assignees = new List<int>
        {
            4521152
        };
        tags = new List<string>
        {
            "system-created"
        };
    }
    public string name { get; set; }
    public string description { get; set; }
    public List<int> assignees { get; set; }
    public List<string> tags { get; set; }
    public string status { get; set; } = "PLAN BEKLİYOR";
    public int priority { get; set; } = 3;
    public long due_date { get; set; } = DateTime.Today.ToEpochMilliseconds();
    public bool due_date_time { get; set; }
    public long time_estimate { get; set; }
    public long start_date { get; set; } = DateTime.Today.ToEpochMilliseconds();
    public bool start_date_time { get; set; }
    public bool notify_all { get; set; } = true;
    public object parent { get; set; }
    public object links_to { get; set; }
    public bool check_required_custom_fields { get; set; }
    public List<CustomField> custom_fields { get; set; }
}

public class CustomField
{
    public string id { get; set; }
    public string value { get; set; }
}