using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNamesOfColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quality",
                table: "OrderIndices",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderIndices",
                newName: "Quality");
        }
    }
}
