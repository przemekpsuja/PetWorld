using Azure;
using Azure.AI.OpenAI;
using OpenAI;
using OpenAI.Chat;
using PetWorld.Application.Configuration;
using PetWorld.Application.Services.Agents.Models;
using System.Text.RegularExpressions;

namespace PetWorld.Application.Services.Agents;

public class CriticAgent
{
    private readonly ChatClient _chatClient;
    private readonly AgentConfiguration _config;

    public CriticAgent(AgentConfiguration config)
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

    public async Task<CriticFeedback> EvaluateAsync(string writerAnswer, CancellationToken ct)
    {
        var userPrompt = $@"ODPOWIEDŹ DO OCENY:
{writerAnswer}

Oceń tę odpowiedź według kryteriów z instrukcji systemowej.";

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(GetSystemPrompt()),
            new UserChatMessage(userPrompt)
        };

        var response = await _chatClient.CompleteChatAsync(messages, cancellationToken: ct);

        var responseText = response.Value.Content[0].Text;

        return ParseResponse(responseText);
    }

    private CriticFeedback ParseResponse(string response)
    {
        try
        {
            var match = Regex.Match(
                response,
                @"approved:\s*(true|false)\s*[\n\r]+feedback:\s*(.+)",
                RegexOptions.Singleline | RegexOptions.IgnoreCase
            );

            if (match.Success)
            {
                return new CriticFeedback
                {
                    Approved = bool.Parse(match.Groups[1].Value.ToLower()),
                    Feedback = match.Groups[2].Value.Trim()
                };
            }
        }
        catch (Exception)
        {
            // If parsing fails, return default feedback
        }

        // Default fallback if parsing fails
        return new CriticFeedback
        {
            Approved = false,
            Feedback = "Nie udało się sparsować odpowiedzi recenzenta. Proszę spróbować ponownie."
        };
    }

    private string GetSystemPrompt()
    {
        return @"Jesteś recenzentem jakości odpowiedzi dla klientów PetWorld.

KRYTERIA OCENY:
1. Logika: Czy odpowiedź ma sens?
2. Trafność produktów: Czy rekomendacje są odpowiednie?
3. Kompletność: Czy zawiera wszystkie potrzebne informacje?
4. Ton: Profesjonalny, ale przyjazny?
5. Jasność: Łatwo zrozumiała dla klienta?

FORMAT ODPOWIEDZI (MUSISZ ŚCIŚLE PRZESTRZEGAĆ):
approved: true/false
feedback: [konkretny komentarz]

ZASADY:
- Użyj ""approved: true"" jeśli odpowiedź spełnia wszystkie kryteria
- Użyj ""approved: false"" jeśli potrzebne są poprawki
- Zawsze podaj konkretną, konstruktywną opinię w feedback
- Bądź wymagający, ale sprawiedliwy
- Feedback powinien być w języku polskim

PRZYKŁADY:

approved: true
feedback: Doskonała odpowiedź - wszystkie kryteria spełnione. Rekomendacje są trafne, ton przyjazny, informacje kompletne.

approved: false
feedback: Odpowiedź jest zbyt ogólna. Proszę polecić konkretne produkty z katalogu i uzasadnić wybór.";
    }
}
