using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosSystem.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrateModelSaleDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_idUser",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.RenameTable(
                name: "Sales",
                newName: "Sale");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_idUser",
                table: "Sale",
                newName: "IX_Sale_idUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sale",
                table: "Sale",
                column: "idSale");

            migrationBuilder.CreateTable(
                name: "SaleDetail",
                columns: table => new
                {
                    idSaleDetail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idSale = table.Column<int>(type: "int", nullable: false),
                    idProduct = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", unicode: false, precision: 18, scale: 2, nullable: false),
                    count = table.Column<int>(type: "int", unicode: false, nullable: false, defaultValue: 1),
                    discount = table.Column<decimal>(type: "decimal(18,2)", unicode: false, nullable: false, defaultValue: 0m),
                    total = table.Column<decimal>(type: "decimal(18,2)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetail", x => x.idSaleDetail);
                    table.ForeignKey(
                        name: "FK_SaleDetail_Products_idProduct",
                        column: x => x.idProduct,
                        principalTable: "Products",
                        principalColumn: "idProduct",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetail_Sale_idSale",
                        column: x => x.idSale,
                        principalTable: "Sale",
                        principalColumn: "idSale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sale_bill",
                table: "Sale",
                column: "bill",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_idProduct",
                table: "SaleDetail",
                column: "idProduct");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_idSale",
                table: "SaleDetail",
                column: "idSale");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Users_idUser",
                table: "Sale",
                column: "idUser",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Users_idUser",
                table: "Sale");

            migrationBuilder.DropTable(
                name: "SaleDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sale",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Sale_bill",
                table: "Sale");

            migrationBuilder.RenameTable(
                name: "Sale",
                newName: "Sales");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_idUser",
                table: "Sales",
                newName: "IX_Sales_idUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                column: "idSale");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_idUser",
                table: "Sales",
                column: "idUser",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
