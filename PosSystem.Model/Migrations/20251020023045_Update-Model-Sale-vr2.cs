using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosSystem.Model.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelSalevr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "SaleDetails",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "SaleDetails",
                newName: "Count");
        }
    }
}
