using ClickUpTaskCreator.Cli.Dtos;
using ClickUpTaskCreator.Cli.Helpers;
using ClickUpTaskCreator.Cli.Options;
using Microsoft.Extensions.Logging;
using System.CommandLine.Invocation;
using System.Text;
using System.Text.Json;

namespace ClickUpTaskCreator.Cli.Commands
{
    public class CreateTaskCommandHandler : ICommandHandler
    {
        private readonly OptionCollection _options;
        private readonly ClickUpConfiguration _configuration;
        private readonly ILogger<CreateTaskCommandHandler> _logger;
        private readonly HttpClient _client;

        public CreateTaskCommandHandler(OptionCollection options,
                                        IHttpClientFactory clientFactory,
                                        ClickUpConfiguration configuration,
                                        ILogger<CreateTaskCommandHandler> logger)
        {
            _options = options;
            _configuration = configuration;
            _logger = logger;
            _client = clientFactory.CreateClient();
        }
        public int Invoke(InvocationContext context)
        {
            
            Console.WriteLine("Worked");
            return 0;
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var title = context.GetOptionValue<string>(_options, "title");
            var description = context.GetOptionValue<string>(_options, "description");
            var customerName = context.GetOptionValue<string>(_options, "customer");
            _client.DefaultRequestHeaders.Add("Authorization", _configuration.ApiKey);
            string payload =JsonSerializer.Serialize( new ClickUpTask(title, description,customerName));
            var postData = new StringContent(payload,Encoding.UTF8,"application/json");
            var ListId = _configuration.ListId;
            var httpResponse = await _client.PostAsync($"{_configuration.ApiUrl}/api/v2/list/{ListId}/task", postData);
            
            var response = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode == false)
            {
                _logger.LogError(response);
            }
            

            return 0;
        }
    }
}