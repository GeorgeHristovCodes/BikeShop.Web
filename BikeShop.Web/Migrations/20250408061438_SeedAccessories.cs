using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BikeShop.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedAccessories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accessories",
                columns: new[] { "Id", "Brand", "Category", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "DRAG", 0, "Лека и удобна каска с вентилационни отвори", "/images/accessories/helmet1.jpg", "DRAG Каска PRO", 129.99m, 15 },
                    { 2, "NS BIKES", 1, "Противохлъзгаща вътрешност и дишаща материя", "/images/accessories/gloves1.jpg", "NS Rъкавици GripX", 39.50m, 30 },
                    { 3, "SPECIALIZED", 2, "Удобна и здрава ръчна помпа", "/images/accessories/pump1.jpg", "SPECIALIZED Помпа AirTool", 49.00m, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessories");
        }
    }
}
