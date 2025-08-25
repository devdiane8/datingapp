using System;

namespace API.Entities;

public class AppUser
{

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string DisplayName { get; set; }
    public required string Email { get; set; }

    public string? ImageUrl { get; set; }

    public required Byte[] PasswordHash { get; set; }
    public required Byte[] PasswordSalt { get; set; }

    //navigation properties
    public Member Member { get; set; } = null!;

}
