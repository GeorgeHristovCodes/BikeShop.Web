using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeShop.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessoryIdToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Bicycles_BicycleId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "BicycleId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AccessoryId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccessoryId",
                table: "Orders",
                column: "AccessoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Accessories_AccessoryId",
                table: "Orders",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Bicycles_BicycleId",
                table: "Orders",
                column: "BicycleId",
                principalTable: "Bicycles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Accessories_AccessoryId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Bicycles_BicycleId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AccessoryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AccessoryId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "BicycleId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Bicycles_BicycleId",
                table: "Orders",
                column: "BicycleId",
                principalTable: "Bicycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
