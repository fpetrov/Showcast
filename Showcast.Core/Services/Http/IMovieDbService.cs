using Showcast.Core.Entities.Media;

namespace Showcast.Core.Services.Http;

public interface IMovieDbService
{
    public Task<Movie?> GetById(string id);
    public Task<Movie?> GetByTitle(string title);
}