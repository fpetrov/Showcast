using System.Net.Http.Json;
using Showcast.Core.Entities.Media;
using Showcast.Core.Messaging.Responses.Recommendation;
using Showcast.Core.Services.Http;

namespace Showcast.Infrastructure.Services.Http;

public class RecommendationService : IRecommendationService
{
    private readonly HttpClient _httpClient;

    public RecommendationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
        _httpClient.BaseAddress ??= new Uri($"http://localhost:5002/");
    }

    public async Task<HttpResponseMessage> GetRecommendations(int[] likedMoviesIds) 
        => await _httpClient.PostAsJsonAsync("recommendations/", likedMoviesIds);

    public async Task<RelativeMoviesResponse[]?> GetRelativeMovies(string movieName) 
        => await _httpClient.GetFromJsonAsync<RelativeMoviesResponse[]?>($"relative?n={movieName}");
}