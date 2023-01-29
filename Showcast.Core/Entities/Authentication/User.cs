using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Showcast.Core.Entities.Media;

namespace Showcast.Core.Entities.Authentication;

public record User(
    [property: Key]
    int Id,
    string Name,
    [property: JsonIgnore]
    string Password,
    Role Role,
    List<RefreshToken> RefreshTokens,
    List<Movie> LikedMovies,
    List<Movie> PlannedMovies
    //List<Review> Reviews
);