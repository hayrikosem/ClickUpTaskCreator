using ClickUpTaskCreator.Cli.Options;
using System.CommandLine;
using System.Net.Http;

namespace ClickUpTaskCreator.Cli.Commands
{

    public class CreateTaskCommand : Command
    {
        const string name = "create";
        const string description = "creates a task in ClickUp";
        public CreateTaskCommand(CreateTaskCommandHandler handler, OptionCollection options ) : base(name, description)
        {
            Handler = handler;
            AddOption(new Option<string>("--title", "Title of the Task.")
            {
                IsRequired = true
            });
            AddOption(new Option<string>("--description", "Description of the task")
            {
                IsRequired=true
            });
            AddOption(new Option<string>("--customer", "Customer information.")
            {
                IsRequired=true
            });
            options.AddRange(Options);
        }
        
    }
}