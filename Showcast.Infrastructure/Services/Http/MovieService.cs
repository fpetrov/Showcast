using Showcast.Core.Entities.Authentication;
using Showcast.Core.Entities.Media;
using Showcast.Core.Services.Http;

namespace Showcast.Infrastructure.Services.Http;

public class MovieService : IMovieService
{
    private readonly IRecommendationService _recommendationService;
    private readonly IMovieDbService _movieDbService;

    public MovieService(IRecommendationService recommendationService, IMovieDbService movieDbService)
    {
        _recommendationService = recommendationService;
        _movieDbService = movieDbService;
    }

    public IAsyncEnumerable<Movie?> GetRelative(int id)
    {
        throw new NotImplementedException();
    }

    public async IAsyncEnumerable<Movie?> GetRelative(string name)
    {
        var response = await _recommendationService.GetRelativeMovies(name);

        foreach (var relativeMovie in response!)
        {
            // var title = string.Join(" ", relativeMovie.Title.Split(" ")[..^1]);

            var movie = await _movieDbService.GetByTitle(relativeMovie.Title);

            yield return movie;
        }
    }

    public Task<Movie?> GetMovie(string name)
    {
        return _movieDbService.GetByTitle(name);
    }

    public async IAsyncEnumerable<Movie?> GetRecommendations(User user)
    {
        var titles = string.Join(',', user.LikedMovies);

        var recommendations = await _recommendationService.GetRecommendations(titles);
        
        foreach (var relativeMovie in recommendations!)
        {
            var movie = await _movieDbService.GetByTitle(relativeMovie);

            yield return movie;
        }
    }
}