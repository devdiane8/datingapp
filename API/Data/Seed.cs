using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Dtos;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDBContext context)
    {
        if (await context.Users.AnyAsync()) return;
        var memberData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var members = JsonSerializer.Deserialize<List<SeedUserDto>>(memberData);
        if (members == null)
            Console.WriteLine("No members found in the seed data.");
        else
        {
            using var hmac = new HMACSHA512();
            foreach (var member in members)
            {
                var user = new AppUser
                {
                    DisplayName = member.DisplayName,
                    Id = member.Id,
                    Email = member.Email,
                    ImageUrl = member.ImageUrl,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                    PasswordSalt = hmac.Key,
                    Member = new Member
                    {
                        Id = member.Id,
                        DateOfBirth = member.DateOfBirth,
                        DisplayName = member.DisplayName,
                        Gender = member.Gender,
                        City = member.City,
                        Country = member.Country,
                        Description = member.Description,
                        ImageUrl = member.ImageUrl
                    }
                };
                user.Member.Photos.Add(new Photo
                {

                    Url = user.ImageUrl ?? "https://randomuser.me/api/portraits/men/11.jpg",
                    MemberId = member.Id
                });
                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }

}
