using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BikeShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedSaleBikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bicycles",
                columns: new[] { "Id", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 3, "Шосеен велосипед с карбонова рамка.", "https://via.placeholder.com/300x200", true, "Road Bike Pro", 1200m, 1 },
                    { 4, "Удобен градски велосипед за ежедневна употреба.", "https://via.placeholder.com/300x200", true, "City Cruiser", 750m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
