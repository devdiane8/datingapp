using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<AppUser> Users { get; set; }
}
