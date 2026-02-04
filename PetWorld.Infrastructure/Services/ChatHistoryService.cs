using Microsoft.EntityFrameworkCore;
using PetWorld.Application.Contracts;
using PetWorld.Domain.Entities;
using PetWorld.Infrastructure.Data;

namespace PetWorld.Infrastructure.Services;

public class ChatHistoryService : IChatHistoryService
{
    private readonly PetWorldDbContext _dbContext;

    public ChatHistoryService(PetWorldDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ChatHistory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.ChatHistory
            .OrderByDescending(ch => ch.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveAsync(string question, string answer, int iterationCount, CancellationToken cancellationToken = default)
    {
        var chatHistory = new ChatHistory
        {
            Date = DateTime.Now,
            Question = question,
            Answer = answer,
            IterationCount = iterationCount
        };

        _dbContext.ChatHistory.Add(chatHistory);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
