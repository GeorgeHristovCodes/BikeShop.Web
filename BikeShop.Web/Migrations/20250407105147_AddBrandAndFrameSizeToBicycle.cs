using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BikeShop.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandAndFrameSizeToBicycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bicycles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Bicycles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FrameSize",
                table: "Bicycles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Bicycles");

            migrationBuilder.DropColumn(
                name: "FrameSize",
                table: "Bicycles");

            migrationBuilder.InsertData(
                table: "Bicycles",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "Quantity", "Type" },
                values: new object[,]
                {
                    { 1, 0, "Планински велосипед под наем", "https://via.placeholder.com/300x200", true, "Rent Bike 1", 15m, 0, 0 },
                    { 2, 0, "Градски велосипед под наем", "https://via.placeholder.com/300x200", true, "Rent Bike 2", 10m, 0, 0 },
                    { 3, 0, "Шосеен велосипед с карбонова рамка.", "https://via.placeholder.com/300x200", true, "Road Bike Pro", 1200m, 0, 1 },
                    { 4, 0, "Удобен градски велосипед за ежедневна употреба.", "https://via.placeholder.com/300x200", true, "City Cruiser", 750m, 0, 1 }
                });
        }
    }
}
