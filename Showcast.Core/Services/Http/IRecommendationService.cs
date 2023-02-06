using Showcast.Core.Messaging.Responses.Recommendation;

namespace Showcast.Core.Services.Http;

public interface IRecommendationService
{
    public Task<HttpResponseMessage> GetRecommendations(int[] likedMoviesIds);

    public Task<RelativeMoviesResponse[]?> GetRelativeMovies(string movieName);
}
