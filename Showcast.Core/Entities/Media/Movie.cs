using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Showcast.Core.Entities.Media;

public record Movie(
    [property: Key] int Id,
    string Name,
    string Genre,
    DateTime Released,
    string Actors,
    string Director,
    string Plot,
    string PosterAddress,
    string BoxOffice,
    [property: JsonPropertyName("imdbRating")]
    double Rating,
    List<Review> Reviews
);