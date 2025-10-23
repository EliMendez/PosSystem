using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosSystem.Model.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelSalevr3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Sales",
                type: "decimal(8,2)",
                unicode: false,
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldUnicode: false,
                oldPrecision: 4,
                oldScale: 2,
                oldDefaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Sales",
                type: "decimal(4,2)",
                unicode: false,
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldUnicode: false,
                oldPrecision: 8,
                oldScale: 2,
                oldDefaultValue: 0m);
        }
    }
}
