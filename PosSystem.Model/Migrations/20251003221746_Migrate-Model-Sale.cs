using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosSystem.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrateModelSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    idSale = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bill = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    saleDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    dni = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    customer = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    discount = table.Column<decimal>(type: "decimal(4,2)", unicode: false, precision: 4, scale: 2, nullable: false, defaultValue: 0m),
                    total = table.Column<decimal>(type: "decimal(18,2)", unicode: false, precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    idUser = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    annulledDate = table.Column<DateOnly>(type: "date", nullable: false),
                    reason = table.Column<string>(type: "TEXT", nullable: true),
                    userCancel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.idSale);
                    table.ForeignKey(
                        name: "FK_Sales_Users_idUser",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_idUser",
                table: "Sales",
                column: "idUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
