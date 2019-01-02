using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiC.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinishingId",
                table: "Price",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Price",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderName = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    cost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderId",
                table: "Product",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Price_FinishingId",
                table: "Price",
                column: "FinishingId");

            migrationBuilder.CreateIndex(
                name: "IX_Price_MaterialId",
                table: "Price",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Finishing_FinishingId",
                table: "Price",
                column: "FinishingId",
                principalTable: "Finishing",
                principalColumn: "FinishingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Material_MaterialId",
                table: "Price",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Order_OrderId",
                table: "Product",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Price_Finishing_FinishingId",
                table: "Price");

            migrationBuilder.DropForeignKey(
                name: "FK_Price_Material_MaterialId",
                table: "Price");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Order_OrderId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Product_OrderId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Price_FinishingId",
                table: "Price");

            migrationBuilder.DropIndex(
                name: "IX_Price_MaterialId",
                table: "Price");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FinishingId",
                table: "Price");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Price");
        }
    }
}
