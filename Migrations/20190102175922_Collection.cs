using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiC.Migrations
{
    public partial class Collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    CollectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    collectionName = table.Column<string>(nullable: true),
                    aestheticParameter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.CollectionId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Collection_CollectionId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropIndex(
                name: "IX_Product_CollectionId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Product");
        }
    }
}
