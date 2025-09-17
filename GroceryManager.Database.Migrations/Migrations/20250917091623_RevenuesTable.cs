using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryManager.Database.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class RevenuesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Supermarkets_SupermarketId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "SupermarketId",
                table: "Items",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Supermarkets_SupermarketId",
                table: "Items",
                column: "SupermarketId",
                principalTable: "Supermarkets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Supermarkets_SupermarketId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "SupermarketId",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Supermarkets_SupermarketId",
                table: "Items",
                column: "SupermarketId",
                principalTable: "Supermarkets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
