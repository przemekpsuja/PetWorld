using Azure;
using Azure.AI.OpenAI;
using OpenAI;
using OpenAI.Chat;
using PetWorld.Application.Configuration;

namespace PetWorld.Application.Services.Agents;

public class ChatClientFactory
{
    private readonly AgentConfiguration _config;

    public ChatClientFactory(AgentConfiguration config)
    {
        _config = config;
    }

    public ChatClient CreateChatClient()
    {
        if (_config.ApiProvider == "AzureOpenAI")
        {
            var azureClient = new AzureOpenAIClient(
                new Uri(_config.Endpoint),
                new AzureKeyCredential(_config.ApiKey));
            return azureClient.GetChatClient(_config.DeploymentName);
        }
        else
        {
            var openAIClient = new OpenAIClient(_config.ApiKey);
            return openAIClient.GetChatClient(_config.Model);
        }
    }
}
