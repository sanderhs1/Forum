using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Forum.Models;
using static System.Net.Mime.MediaTypeNames;

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
    public DbSet<RentListing> StartDate { get; set; }
    public DbSet<RentListing> EndDate { get; set; }
    public DbSet<UploadedImage> UploadedImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}