using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNamesOfColumns3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderIndices_Orders_OrderId",
                table: "OrderIndices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderIndices",
                table: "OrderIndices");

            migrationBuilder.RenameTable(
                name: "OrderIndices",
                newName: "OrderLines");

            migrationBuilder.RenameIndex(
                name: "IX_OrderIndices_OrderId",
                table: "OrderLines",
                newName: "IX_OrderLines_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Orders_OrderId",
                table: "OrderLines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderId",
                table: "OrderLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines");

            migrationBuilder.RenameTable(
                name: "OrderLines",
                newName: "OrderIndices");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderIndices",
                newName: "IX_OrderIndices_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderIndices",
                table: "OrderIndices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderIndices_Orders_OrderId",
                table: "OrderIndices",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
