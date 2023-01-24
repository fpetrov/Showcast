using Showcast.Core.Services.Security;

namespace Showcast.Infrastructure.Services.Security;

public class HashService : IHashService
{
    public bool Verify(string content, string hash) => BCrypt.Net.BCrypt.Verify(content, hash);

    public string Hash(string content) => BCrypt.Net.BCrypt.HashPassword(content);
}