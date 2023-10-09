using System;
using Microsoft.EntityFrameworkCore;

namespace Forum.Models;

public class ListingsDbContext : DbContext
{
    public ListingsDbContext(DbContextOptions<ListingsDbContext> options) : base(options)
    {
        Database.EnsureCreated(); // Feil med database, trenger fiks, kan ikke åpne Listings page, fullfør modul demo-dynamic-views etc
    }

    public DbSet<Listings> Listings { get; set; }
}