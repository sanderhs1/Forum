using System;
using Microsoft.EntityFrameworkCore;

namespace Renting.Models;

public class ListingDbContext : DbContext
{
    public ListingDbContext(DbContextOptions<ListingDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Listing> Listings { get; set; }
}