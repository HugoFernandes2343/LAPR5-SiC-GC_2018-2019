using Microsoft.EntityFrameworkCore.Migrations;

namespace SiC.Migrations
{
    public partial class CatalogProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Catalog_CatalogId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CatalogId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CatalogId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "CatalogProduct",
                columns: table => new
                {
                    CatalogId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogProduct", x => new { x.CatalogId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CatalogProduct_Catalog_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogProduct_ProductId",
                table: "CatalogProduct",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogProduct");

            migrationBuilder.AddColumn<int>(
                name: "CatalogId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CatalogId",
                table: "Product",
                column: "CatalogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Catalog_CatalogId",
                table: "Product",
                column: "CatalogId",
                principalTable: "Catalog",
                principalColumn: "CatalogId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
