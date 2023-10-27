using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class RemDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "RentListing");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "RentListing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Rents",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
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
        }
    }
}
