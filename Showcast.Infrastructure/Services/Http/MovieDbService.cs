using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Showcast.Core.Entities.Media;
using Showcast.Core.Services.Http;

namespace Showcast.Infrastructure.Services.Http;

/// <summary>
/// Http client for OMDb site where all movie data located.
/// </summary>
public class MovieDbService : IMovieDbService
{
    private readonly HttpClient _httpClient;
    
    public MovieDbService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        var token = configuration["OMDbToken"];

        _httpClient.BaseAddress ??= new Uri($"https://omdbapi.com/?apikey={token}&");
    }

    public async Task<Movie?> GetById(string id) => await _httpClient.GetFromJsonAsync<Movie>(_httpClient.BaseAddress + "i=" + id);
    public async Task<Movie?> GetByTitle(string title) => await _httpClient.GetFromJsonAsync<Movie>(_httpClient.BaseAddress + "t=" + title);
}