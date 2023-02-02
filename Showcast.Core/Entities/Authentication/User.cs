using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Showcast.Core.Entities.Authentication;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore] 
    public string Password { get; set; } 
    public Role Role { get; set; }

    public List<RefreshToken> RefreshTokens { get; } = new();
    // public List<Movie> LikedMovies { get; set; }
    // public List<Movie> PlannedMovies { get; set; }
    // List<Review> Reviews
}