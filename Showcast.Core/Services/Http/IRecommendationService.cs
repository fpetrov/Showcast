using Showcast.Core.Messaging.Responses.Recommendation;

namespace Showcast.Core.Services.Http;

public interface IRecommendationService
{
    public Task<string[]?> GetRecommendations(string likedMoviesTitles);

    public Task<RelativeMoviesResponse[]?> GetRelativeMovies(string movieName);
}
