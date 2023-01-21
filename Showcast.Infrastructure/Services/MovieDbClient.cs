using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Showcast.Core.Entities.Media;
using Showcast.Infrastructure.Repositories.User;

namespace Showcast.Infrastructure.Services;

/// <summary>
/// Http client for OMDb site where all movie data located.
/// </summary>
public class MovieDbClient
{
    private readonly HttpClient _httpClient;
    
    public MovieDbClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        var token = configuration["OMDbToken"];

        _httpClient.BaseAddress ??= new Uri($"https://omdbapi.com/?apikey={token}&");

        var rep = new UserRepository(null);
        
        
    }

    public async Task<Movie?> GetById(string id) => await _httpClient.GetFromJsonAsync<Movie>("i=" + id);
    public async Task<Movie?> GetByTitle(string title) => await _httpClient.GetFromJsonAsync<Movie>("t=" + title);
}