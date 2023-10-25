using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class ListingDbExpanded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentListings_Listings_ListingId",
                table: "RentListings");

            migrationBuilder.DropForeignKey(
                name: "FK_RentListings_Rents_RentId",
                table: "RentListings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentListings",
                table: "RentListings");

            migrationBuilder.RenameTable(
                name: "RentListings",
                newName: "RentListing");

            migrationBuilder.RenameColumn(
                name: "RentDate",
                table: "Rents",
                newName: "CheckOutDate");

            migrationBuilder.RenameIndex(
                name: "IX_RentListings_RentId",
                table: "RentListing",
                newName: "IX_RentListing_RentId");

            migrationBuilder.RenameIndex(
                name: "IX_RentListings_ListingId",
                table: "RentListing",
                newName: "IX_RentListing_ListingId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Rents",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "RentListing",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "RentListing",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentListing",
                table: "RentListing",
                column: "RentListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentListing_Listings_ListingId",
                table: "RentListing",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "ListingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentListing_Rents_RentId",
                table: "RentListing",
                column: "RentId",
                principalTable: "Rents",
                principalColumn: "RentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentListing_Listings_ListingId",
                table: "RentListing");

            migrationBuilder.DropForeignKey(
                name: "FK_RentListing_Rents_RentId",
                table: "RentListing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentListing",
                table: "RentListing");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "RentListing");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "RentListing");

            migrationBuilder.RenameTable(
                name: "RentListing",
                newName: "RentListings");

            migrationBuilder.RenameColumn(
                name: "CheckOutDate",
                table: "Rents",
                newName: "RentDate");

            migrationBuilder.RenameIndex(
                name: "IX_RentListing_RentId",
                table: "RentListings",
                newName: "IX_RentListings_RentId");

            migrationBuilder.RenameIndex(
                name: "IX_RentListing_ListingId",
                table: "RentListings",
                newName: "IX_RentListings_ListingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentListings",
                table: "RentListings",
                column: "RentListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentListings_Listings_ListingId",
                table: "RentListings",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "ListingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentListings_Rents_RentId",
                table: "RentListings",
                column: "RentId",
                principalTable: "Rents",
                principalColumn: "RentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
