namespace Showcast.Infrastructure.Services.Http;

public class RecommendationService
{
    private readonly HttpClient _httpClient;

    public RecommendationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
        _httpClient.BaseAddress ??= new Uri($"http://localhost:5002");
    }

    public Task GetRecommendations()
    {
        return Task.CompletedTask;
    }
}