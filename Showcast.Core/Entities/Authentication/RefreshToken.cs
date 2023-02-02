using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Showcast.Core.Entities.Authentication;

[Owned]
public class RefreshToken
{
    [Key, JsonIgnore]
    public int Id { get; set; }
    public string? Body { get; set; }
    public string? Fingerprint { get; set; }
    public DateTime Expires { get; set; }
    
    public bool IsActive => !(DateTime.UtcNow >= Expires);
}