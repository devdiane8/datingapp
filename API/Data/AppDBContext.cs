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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
