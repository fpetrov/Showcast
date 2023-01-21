using System.ComponentModel.DataAnnotations;
using Showcast.Core.Entities.Authentication;

namespace Showcast.Core.Entities.Media;

public record Review(
    [property: Key]
    int Id,
    User Author,
    Movie Movie,
    DateTime Published,
    string Text,
    [property: Range(0, 5)]
    int Rating
);