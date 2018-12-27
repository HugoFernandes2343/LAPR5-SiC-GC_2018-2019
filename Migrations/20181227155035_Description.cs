using Microsoft.EntityFrameworkCore.Migrations;

namespace SiC.Migrations
{
    public partial class Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Restriction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Material",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Finishing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CatalogDescription",
                table: "Catalog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CatalogName",
                table: "Catalog",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Restriction");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Finishing");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CatalogDescription",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "CatalogName",
                table: "Catalog");
        }
    }
}
