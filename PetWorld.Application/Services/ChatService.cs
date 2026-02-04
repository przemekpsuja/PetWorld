using Microsoft.Extensions.Logging;
using PetWorld.Application.Configuration;
using PetWorld.Application.Contracts;
using PetWorld.Application.Services.Agents;

namespace PetWorld.Application.Services;

public class ChatService : Contracts.IChatService
{
    private readonly AgentConfiguration _config;
    private readonly IProductRepository _productRepository;
    private readonly WriterAgent _writerAgent;
    private readonly CriticAgent _criticAgent;
    private readonly ILogger<ChatService> _logger;

    public ChatService(
        AgentConfiguration config,
        IProductRepository productRepository,
        WriterAgent writerAgent,
        CriticAgent criticAgent,
        ILogger<ChatService> logger)
    {
        _config = config;
        _productRepository = productRepository;
        _writerAgent = writerAgent;
        _criticAgent = criticAgent;
        _logger = logger;
    }

    public async Task<ChatResponse> GetResponseAsync(string question, CancellationToken ct)
    {
        _logger.LogInformation("Starting chat response generation for question: {Question}", question);

        try
        {
            // Fetch products from database
            var products = await _productRepository.GetAllProductsAsync(ct);

            _logger.LogInformation("Fetched {ProductCount} products from database", products.Count);

            string writerAnswer = string.Empty;
            string? feedback = null;
            int iteration = 0;

            // Writer → Critic loop (max MaxIterations)
            for (iteration = 1; iteration <= _config.MaxIterations; iteration++)
            {
                _logger.LogInformation("Starting iteration {Iteration}/{MaxIterations}", iteration, _config.MaxIterations);

                // Writer generates answer
                writerAnswer = await _writerAgent.GenerateAnswerAsync(
                    question,
                    products,
                    feedback,
                    ct);

                _logger.LogInformation("Writer generated answer (length: {Length} chars)", writerAnswer.Length);

                // Critic evaluates answer
                var criticResult = await _criticAgent.EvaluateAsync(writerAnswer, ct);

                _logger.LogInformation(
                    "Critic evaluation - Approved: {Approved}, Feedback: {Feedback}",
                    criticResult.Approved,
                    criticResult.Feedback);

                // If approved, stop early
                if (criticResult.Approved)
                {
                    _logger.LogInformation("Answer approved on iteration {Iteration}", iteration);
                    break;
                }

                // Store feedback for next iteration
                feedback = criticResult.Feedback;
            }

            _logger.LogInformation(
                "Chat response generation completed. Final iteration: {Iteration}",
                iteration);

            return new ChatResponse
            {
                Answer = writerAnswer,
                IterationCount = Math.Min(iteration, _config.MaxIterations)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during chat response generation");
            throw;
        }
    }
}
