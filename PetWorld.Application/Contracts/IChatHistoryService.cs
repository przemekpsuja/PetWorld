using PetWorld.Domain.Entities;

namespace PetWorld.Application.Contracts;

public interface IChatHistoryService
{
    Task<List<ChatHistory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(string question, string answer, int iterationCount, CancellationToken cancellationToken = default);
}
