using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Forum.Models;

public class ListingDbContext : IdentityDbContext
{
    public ListingDbContext(DbContextOptions<ListingDbContext> options) : base(options)
    {
        Database.EnsureCreated(); 
    }

    public DbSet<Listing> Listings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<RentListing> RentListings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}