using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Showcast.Core.Entities.Authentication;

[Owned]
public record RefreshToken([property: Key, JsonIgnore] int Id, string Body, string Fingerprint, DateTime Expires)
{
    public bool IsActive => !(DateTime.UtcNow >= Expires);

    private static readonly RefreshToken Default = new(
        1,
        "Default",
        "Default", 
        DateTime.Now
    );
    
    public RefreshToken() : this(Default)
    {
        
    }
}