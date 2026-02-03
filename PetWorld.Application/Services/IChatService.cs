namespace PetWorld.Application.Services;

public interface IChatService
{
    Task<ChatResponse> GetResponseAsync(string question, CancellationToken ct);
}

public class ChatResponse
{
    public string Answer { get; set; } = string.Empty;
    public int IterationCount { get; set; }
}
