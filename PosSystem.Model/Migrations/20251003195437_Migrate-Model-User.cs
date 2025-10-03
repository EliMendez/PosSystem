using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosSystem.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrateModelUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Roles",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Roles",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Roles",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "IdRole",
                table: "Roles",
                newName: "idRole");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Description",
                table: "Roles",
                newName: "IX_Roles_description");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: false),
                    surname = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: false),
                    idRole = table.Column<int>(type: "int", nullable: false),
                    phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    status = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.idUser);
                    table.ForeignKey(
                        name: "FK_Users_Roles_idRole",
                        column: x => x.idRole,
                        principalTable: "Roles",
                        principalColumn: "idRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_idRole",
                table: "Users",
                column: "idRole");

            migrationBuilder.CreateIndex(
                name: "IX_Users_phone",
                table: "Users",
                column: "phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Roles",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Roles",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Roles",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "idRole",
                table: "Roles",
                newName: "IdRole");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_description",
                table: "Roles",
                newName: "IX_Roles_Description");
        }
    }
}
