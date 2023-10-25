using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Forum.Models;

namespace Forum.DAL;

public class ListingDbContext : IdentityDbContext
{
    public ListingDbContext(DbContextOptions<ListingDbContext> options) : base(options)
    {
        //Database.EnsureCreated(); 
    }

    public DbSet<Listing> Listings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<RentListing> RentListings { get; set; }
    public DbSet<RentListing> CheckInDate { get; set; }
    public DbSet<RentListing> CheckOutDate { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}