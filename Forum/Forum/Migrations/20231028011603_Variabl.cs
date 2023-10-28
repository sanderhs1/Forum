using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class Variabl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysStayed",
                table: "RentListing");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "RentListing",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "RentListing",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "RentListing");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "RentListing");

            migrationBuilder.AddColumn<int>(
                name: "DaysStayed",
                table: "RentListing",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
