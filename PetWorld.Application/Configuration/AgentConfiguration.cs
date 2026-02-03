namespace PetWorld.Application.Configuration;

public class AgentConfiguration
{
    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int MaxIterations { get; set; } = 3;
}
