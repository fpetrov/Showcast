using Showcast.Core.Entities.Authentication;
using Showcast.Core.Entities.Media;

namespace Showcast.Core.Services.Http;

public interface IMovieService
{
    public IAsyncEnumerable<Movie?> GetRelative(int id);
    public IAsyncEnumerable<Movie?> GetRelative(string name);
    public Task<Movie?> GetMovie(string name);

    public IAsyncEnumerable<Movie?> GetRecommendations(User user);
}