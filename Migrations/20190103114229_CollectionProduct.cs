using Microsoft.EntityFrameworkCore.Migrations;

namespace SiC.Migrations
{
    public partial class CollectionProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Collection_CollectionId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CollectionId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "CollectionProduct",
                columns: table => new
                {
                    CollectionId = table.Column<int>(nullable: false),
                    ProdutctId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionProduct", x => new { x.CollectionId, x.ProdutctId });
                    table.ForeignKey(
                        name: "FK_CollectionProduct_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "CollectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionProduct_Product_ProdutctId",
                        column: x => x.ProdutctId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProduct_ProdutctId",
                table: "CollectionProduct",
                column: "ProdutctId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionProduct");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CollectionId",
                table: "Product",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Collection_CollectionId",
                table: "Product",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "CollectionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
