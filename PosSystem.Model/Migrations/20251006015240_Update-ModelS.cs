using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosSystem.Model.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_idCategory",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Users_idUser",
                table: "Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Products_idProduct",
                table: "SaleDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Sale_idSale",
                table: "SaleDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_idRole",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Users",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Users",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "idRole",
                table: "Users",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "idUser",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_phone",
                table: "Users",
                newName: "IX_Users_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_idRole",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "SaleDetail",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "productName",
                table: "SaleDetail",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "SaleDetail",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "SaleDetail",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "count",
                table: "SaleDetail",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "idSale",
                table: "SaleDetail",
                newName: "SaleId");

            migrationBuilder.RenameColumn(
                name: "idProduct",
                table: "SaleDetail",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "idSaleDetail",
                table: "SaleDetail",
                newName: "SaleDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetail_idSale",
                table: "SaleDetail",
                newName: "IX_SaleDetail_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetail_idProduct",
                table: "SaleDetail",
                newName: "IX_SaleDetail_ProductId");

            migrationBuilder.RenameColumn(
                name: "userCancel",
                table: "Sale",
                newName: "UserCancel");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "Sale",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Sale",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "saleDate",
                table: "Sale",
                newName: "SaleDate");

            migrationBuilder.RenameColumn(
                name: "reason",
                table: "Sale",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "dni",
                table: "Sale",
                newName: "Dni");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "Sale",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "customer",
                table: "Sale",
                newName: "Customer");

            migrationBuilder.RenameColumn(
                name: "bill",
                table: "Sale",
                newName: "Bill");

            migrationBuilder.RenameColumn(
                name: "annulledDate",
                table: "Sale",
                newName: "AnnulledDate");

            migrationBuilder.RenameColumn(
                name: "idUser",
                table: "Sale",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "idSale",
                table: "Sale",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_bill",
                table: "Sale",
                newName: "IX_Sale_Bill");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_idUser",
                table: "Sale",
                newName: "IX_Sale_UserId");

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
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_description",
                table: "Roles",
                newName: "IX_Roles_Description");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "Products",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Products",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "salePrice",
                table: "Products",
                newName: "SalePrice");

            migrationBuilder.RenameColumn(
                name: "minimumStock",
                table: "Products",
                newName: "MinimumStock");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Products",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "barcode",
                table: "Products",
                newName: "Barcode");

            migrationBuilder.RenameColumn(
                name: "idCategory",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "idProduct",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_description",
                table: "Products",
                newName: "IX_Products_Description");

            migrationBuilder.RenameIndex(
                name: "IX_Products_barcode",
                table: "Products",
                newName: "IX_Products_Barcode");

            migrationBuilder.RenameIndex(
                name: "IX_Products_idCategory",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "document",
                table: "documentNumbers",
                newName: "Document");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "documentNumbers",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "idDocumentNumber",
                table: "documentNumbers",
                newName: "DocumentNumberId");

            migrationBuilder.RenameIndex(
                name: "IX_documentNumbers_document",
                table: "documentNumbers",
                newName: "IX_documentNumbers_Document");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Categories",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Categories",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "idCategory",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_description",
                table: "Categories",
                newName: "IX_Categories_Description");

            migrationBuilder.RenameColumn(
                name: "ruc",
                table: "Businesses",
                newName: "Ruc");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Businesses",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "owner",
                table: "Businesses",
                newName: "Owner");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Businesses",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "Businesses",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Businesses",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "companyName",
                table: "Businesses",
                newName: "CompanyName");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Businesses",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "idBusiness",
                table: "Businesses",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Businesses_phone",
                table: "Businesses",
                newName: "IX_Businesses_Phone");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Users_UserId",
                table: "Sale",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Products_ProductId",
                table: "SaleDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Sale_SaleId",
                table: "SaleDetail",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Users_UserId",
                table: "Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Products_ProductId",
                table: "SaleDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Sale_SaleId",
                table: "SaleDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Users",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Users",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Users",
                newName: "idRole");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "idUser");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Phone",
                table: "Users",
                newName: "IX_Users_phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                newName: "IX_Users_idRole");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "SaleDetail",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "SaleDetail",
                newName: "productName");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SaleDetail",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "SaleDetail",
                newName: "discount");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "SaleDetail",
                newName: "count");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "SaleDetail",
                newName: "idSale");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "SaleDetail",
                newName: "idProduct");

            migrationBuilder.RenameColumn(
                name: "SaleDetailId",
                table: "SaleDetail",
                newName: "idSaleDetail");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetail_SaleId",
                table: "SaleDetail",
                newName: "IX_SaleDetail_idSale");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetail_ProductId",
                table: "SaleDetail",
                newName: "IX_SaleDetail_idProduct");

            migrationBuilder.RenameColumn(
                name: "UserCancel",
                table: "Sale",
                newName: "userCancel");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Sale",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Sale",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "Sale",
                newName: "saleDate");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "Sale",
                newName: "reason");

            migrationBuilder.RenameColumn(
                name: "Dni",
                table: "Sale",
                newName: "dni");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Sale",
                newName: "discount");

            migrationBuilder.RenameColumn(
                name: "Customer",
                table: "Sale",
                newName: "customer");

            migrationBuilder.RenameColumn(
                name: "Bill",
                table: "Sale",
                newName: "bill");

            migrationBuilder.RenameColumn(
                name: "AnnulledDate",
                table: "Sale",
                newName: "annulledDate");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sale",
                newName: "idUser");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "Sale",
                newName: "idSale");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_Bill",
                table: "Sale",
                newName: "IX_Sale_bill");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_UserId",
                table: "Sale",
                newName: "IX_Sale_idUser");

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
                name: "RoleId",
                table: "Roles",
                newName: "idRole");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Description",
                table: "Roles",
                newName: "IX_Roles_description");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Products",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "SalePrice",
                table: "Products",
                newName: "salePrice");

            migrationBuilder.RenameColumn(
                name: "MinimumStock",
                table: "Products",
                newName: "minimumStock");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Products",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "Barcode",
                table: "Products",
                newName: "barcode");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "idCategory");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "idProduct");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Description",
                table: "Products",
                newName: "IX_Products_description");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Barcode",
                table: "Products",
                newName: "IX_Products_barcode");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_idCategory");

            migrationBuilder.RenameColumn(
                name: "Document",
                table: "documentNumbers",
                newName: "document");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "documentNumbers",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "DocumentNumberId",
                table: "documentNumbers",
                newName: "idDocumentNumber");

            migrationBuilder.RenameIndex(
                name: "IX_documentNumbers_Document",
                table: "documentNumbers",
                newName: "IX_documentNumbers_document");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Categories",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Categories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Categories",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "idCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Description",
                table: "Categories",
                newName: "IX_Categories_description");

            migrationBuilder.RenameColumn(
                name: "Ruc",
                table: "Businesses",
                newName: "ruc");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Businesses",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Businesses",
                newName: "owner");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Businesses",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Businesses",
                newName: "discount");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Businesses",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Businesses",
                newName: "companyName");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Businesses",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Businesses",
                newName: "idBusiness");

            migrationBuilder.RenameIndex(
                name: "IX_Businesses_Phone",
                table: "Businesses",
                newName: "IX_Businesses_phone");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_idCategory",
                table: "Products",
                column: "idCategory",
                principalTable: "Categories",
                principalColumn: "idCategory",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Users_idUser",
                table: "Sale",
                column: "idUser",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Products_idProduct",
                table: "SaleDetail",
                column: "idProduct",
                principalTable: "Products",
                principalColumn: "idProduct",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Sale_idSale",
                table: "SaleDetail",
                column: "idSale",
                principalTable: "Sale",
                principalColumn: "idSale",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_idRole",
                table: "Users",
                column: "idRole",
                principalTable: "Roles",
                principalColumn: "idRole",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
