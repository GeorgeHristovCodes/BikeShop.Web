using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeShop.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessoryFieldsToCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Bicycles_BicycleId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "BicycleId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AccessoryId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_AccessoryId",
                table: "CartItems",
                column: "AccessoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Accessories_AccessoryId",
                table: "CartItems",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Bicycles_BicycleId",
                table: "CartItems",
                column: "BicycleId",
                principalTable: "Bicycles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Accessories_AccessoryId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Bicycles_BicycleId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_AccessoryId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "AccessoryId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "BicycleId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Bicycles_BicycleId",
                table: "CartItems",
                column: "BicycleId",
                principalTable: "Bicycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
