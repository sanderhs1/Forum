﻿// <auto-generated />
using Forum.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Forum.Migrations
{
    [DbContext(typeof(ListingDbContext))]
    [Migration("20231011115918_ShopDbExpanded2")]
    partial class ShopDbExpanded2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("Forum.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Forum.Models.Listing", b =>
                {
                    b.Property<int>("ListingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("ListingId");

                    b.ToTable("Listings");
                });

            modelBuilder.Entity("Forum.Models.Rent", b =>
                {
                    b.Property<int>("RentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RentDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("RentId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("Forum.Models.RentListing", b =>
                {
                    b.Property<int>("RentListingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ListId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ListingId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RentId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("RentListingPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("RentListingId");

                    b.HasIndex("ListingId");

                    b.HasIndex("RentId");

                    b.ToTable("RentListings");
                });

            modelBuilder.Entity("Forum.Models.Rent", b =>
                {
                    b.HasOne("Forum.Models.Customer", "Customer")
                        .WithMany("Rents")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Forum.Models.RentListing", b =>
                {
                    b.HasOne("Forum.Models.Listing", "Listing")
                        .WithMany("RentListings")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Forum.Models.Rent", "Rent")
                        .WithMany("RentListings")
                        .HasForeignKey("RentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");

                    b.Navigation("Rent");
                });

            modelBuilder.Entity("Forum.Models.Customer", b =>
                {
                    b.Navigation("Rents");
                });

            modelBuilder.Entity("Forum.Models.Listing", b =>
                {
                    b.Navigation("RentListings");
                });

            modelBuilder.Entity("Forum.Models.Rent", b =>
                {
                    b.Navigation("RentListings");
                });
#pragma warning restore 612, 618
        }
    }
}
