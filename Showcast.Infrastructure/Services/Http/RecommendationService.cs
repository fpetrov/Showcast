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

    public async Task<string[]?> GetRecommendations(string likedMoviesTitles) 
        => await _httpClient.GetFromJsonAsync<string[]>($"recommendations?n={likedMoviesTitles}");

    public async Task<RelativeMoviesResponse[]?> GetRelativeMovies(string movieName) 
        => await _httpClient.GetFromJsonAsync<RelativeMoviesResponse[]?>($"relative?n={movieName}");
}