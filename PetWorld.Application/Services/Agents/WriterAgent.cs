using Azure;
using Azure.AI.OpenAI;
using OpenAI;
using OpenAI.Chat;
using PetWorld.Application.Configuration;
using PetWorld.Application.DTOs;
using System.Text.Json;

namespace PetWorld.Application.Services.Agents;

public class WriterAgent
{
    private readonly ChatClient _chatClient;
    private readonly AgentConfiguration _config;

    public WriterAgent(AgentConfiguration config)
    {
        _config = config;

        if (_config.ApiProvider == "AzureOpenAI")
        {
            var azureClient = new AzureOpenAIClient(
                new Uri(_config.Endpoint),
                new AzureKeyCredential(_config.ApiKey));
            _chatClient = azureClient.GetChatClient(_config.DeploymentName);
        }
        else
        {
            var openAIClient = new OpenAIClient(_config.ApiKey);
            _chatClient = openAIClient.GetChatClient(_config.Model);
        }
    }

    public async Task<string> GenerateAnswerAsync(
        string question,
        List<ProductDto> products,
        string? previousFeedback,
        CancellationToken ct)
    {
        var productsJson = JsonSerializer.Serialize(products, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

        var userPrompt = $@"PYTANIE KLIENTA:
{question}

DOSTĘPNE PRODUKTY:
{productsJson}";

        if (!string.IsNullOrEmpty(previousFeedback))
        {
            userPrompt += $@"

POPRZEDNIA OPINIA RECENZENTA:
{previousFeedback}

Proszę popraw swoją odpowiedź na podstawie tej opinii.";
        }

        userPrompt += "\n\nWAŻNE: Zwróć WYŁĄCZNIE finalną odpowiedź dla klienta. Bez nagłówków, bez meta-komentarzy.";

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(GetSystemPrompt()),
            new UserChatMessage(userPrompt)
        };

        var response = await _chatClient.CompleteChatAsync(messages, cancellationToken: ct);

        return response.Value.Content[0].Text;
    }

    private string GetSystemPrompt()
    {
        return @"Jesteś asystentem AI w PetWorld - specjalistą od rekomendacji produktów dla zwierząt.

TWOJA ROLA:
- Zrozum potrzeby klienta i polecaj odpowiednie produkty
- Udzielaj kompletnych, przyjaznych, profesjonalnych odpowiedzi
- Skup się na zadowoleniu klienta

ZASADY:
- Zawsze odpowiadaj w języku polskim
- Używaj naturalnego, ciepłego tonu
- Rekomenduj konkretne produkty z dostępnego katalogu
- Wyjaśnij dlaczego dany produkt pasuje do potrzeb klienta
- Jeśli klient pyta o coś, czego nie mamy w katalogu, uprzejmie poinformuj o tym

WAŻNE: Zwracaj WYŁĄCZNIE finalną odpowiedź dla klienta. Bez nagłówków typu '[Writer Agent]', bez meta-komentarzy o iteracjach.";
    }
}
