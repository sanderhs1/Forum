using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCheckInCheckOutDat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysStayed",
                table: "Rents");

            migrationBuilder.AddColumn<int>(
                name: "DaysStayed",
                table: "RentListing",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysStayed",
                table: "RentListing");

            migrationBuilder.AddColumn<int>(
                name: "DaysStayed",
                table: "Rents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
