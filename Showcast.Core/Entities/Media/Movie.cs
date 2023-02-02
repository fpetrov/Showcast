using System.ComponentModel.DataAnnotations;

namespace Showcast.Core.Entities.Media;

public class Movie
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string Genre { get; set; }
    public DateTime Released { get; set; }
    public string Actors { get; set; }
    public string Director { get; set; }
    public string Plot { get; set; }
    public string PosterAddress { get; set; }
    public string BoxOffice { get; set; }
    
    // [JsonPropertyName("imdbRating")]
    public double Rating { get; set; }
}