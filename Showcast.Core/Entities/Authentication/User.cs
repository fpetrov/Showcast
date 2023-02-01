using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Showcast.Core.Entities.Media;

namespace Showcast.Core.Entities.Authentication;

public record User(
    [property: Key] int Id,
    string Name,
    [property: JsonIgnore] string Password,
    Role Role,
    List<RefreshToken> RefreshTokens,
    List<Movie> LikedMovies,
    List<Movie> PlannedMovies
    //List<Review> Reviews
)
{
    private static readonly User Default = new(
        1,
        "Default",
        "Default",
        Role.Default,
        new List<RefreshToken>(),
        new List<Movie>(),
        new List<Movie>()
    );

    public User() : this(Default)
    {
        
    }
}