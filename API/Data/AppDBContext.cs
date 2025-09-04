using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data;

public class AppDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<AppUser> Users { get; set; }

    public DbSet<Member> Members { get; set; }

    public DbSet<Photo> Photos { get; set; }

    public DbSet<MemberLike> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MemberLike>()
                .HasKey(x => new { x.SourceMemberId, x.TargetMemberId });
        modelBuilder.Entity<MemberLike>()
                .HasOne(x => x.SourceMember)
                .WithMany(x => x.LikedByMembers)
                .HasForeignKey(x => x.SourceMemberId)
                .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<MemberLike>()
                .HasOne(x => x.TargetMember)
                .WithMany(x => x.LikedMembers)
                .HasForeignKey(x => x.TargetMemberId)
                .OnDelete(DeleteBehavior.Cascade);
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            x => x.ToUniversalTime(),
            x => DateTime.SpecifyKind(x, DateTimeKind.Utc));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
            }
        }
    }
}