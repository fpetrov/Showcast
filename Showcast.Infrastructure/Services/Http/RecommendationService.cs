using System.Net.Http.Json;

namespace Showcast.Infrastructure.Services.Http;

public class RecommendationService
{
    private readonly HttpClient _httpClient;

    public RecommendationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
        _httpClient.BaseAddress ??= new Uri($"http://localhost:5002/");
    }

    public async Task<HttpResponseMessage> GetRecommendations(int[] likedMoviesIds) 
        => await _httpClient.PostAsJsonAsync("recommendations/", likedMoviesIds);
    
    public async Task<HttpResponseMessage> GetRelativeMovies(int movieId) 
        => await _httpClient.PostAsJsonAsync("relative/", movieId);
}