using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiC.Migrations
{
    public partial class basededados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acabamentos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acabamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcabamentosMaterial",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaterialAcabamentoId = table.Column<long>(nullable: false),
                    AcabamentoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcabamentosMaterial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    SuperCategoriaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dimensoes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    altura = table.Column<long>(nullable: false),
                    alturaMax = table.Column<long>(nullable: false),
                    alturaMin = table.Column<long>(nullable: false),
                    largura = table.Column<long>(nullable: false),
                    larguraMax = table.Column<long>(nullable: false),
                    larguraMin = table.Column<long>(nullable: false),
                    profundidade = table.Column<long>(nullable: false),
                    profundidadeMax = table.Column<long>(nullable: false),
                    profundidadeMin = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materiais",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MateriaisAcabamentos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    MaterialId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaisAcabamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MateriaisProduto",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProdutoId = table.Column<long>(nullable: false),
                    MaterialAcabamentoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaisProduto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProdutoId = table.Column<long>(nullable: false),
                    ParteId = table.Column<long>(nullable: false),
                    Obrigatoria = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    CategoriaId = table.Column<long>(nullable: false),
                    Preco = table.Column<long>(nullable: false),
                    maxTaxaOcupacao = table.Column<long>(nullable: false),
                    maxTaxaAtual = table.Column<long>(nullable: false),
                    RestringirMateriais = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategorias",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoriaId = table.Column<long>(nullable: false),
                    SubCatId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategorias", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acabamentos");

            migrationBuilder.DropTable(
                name: "AcabamentosMaterial");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Dimensoes");

            migrationBuilder.DropTable(
                name: "Materiais");

            migrationBuilder.DropTable(
                name: "MateriaisAcabamentos");

            migrationBuilder.DropTable(
                name: "MateriaisProduto");

            migrationBuilder.DropTable(
                name: "Partes");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "SubCategorias");
        }
    }
}
