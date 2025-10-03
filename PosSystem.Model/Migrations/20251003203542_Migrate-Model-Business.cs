using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosSystem.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrateModelBusiness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    idBusiness = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ruc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    companyName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    owner = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    discount = table.Column<decimal>(type: "decimal(4,2)", unicode: false, precision: 4, scale: 2, nullable: false, defaultValue: 0m),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.idBusiness);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_phone",
                table: "Businesses",
                column: "phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Businesses");
        }
    }
}
