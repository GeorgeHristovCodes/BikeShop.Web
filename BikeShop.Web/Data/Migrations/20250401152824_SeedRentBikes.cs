using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BikeShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRentBikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Bicycles",
                columns: new[] { "Id", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "Планински велосипед под наем", "https://via.placeholder.com/300x200", true, "Rent Bike 1", 15m, 2 },
                    { 2, "Градски велосипед под наем", "https://via.placeholder.com/300x200", true, "Rent Bike 2", 10m, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
