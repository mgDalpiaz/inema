using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Repository.SqlServer.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogoEntidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IntegrationCode = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    InsertedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    RemovedAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    LastUpdatedUserId = table.Column<Guid>(nullable: true),
                    Commited = table.Column<bool>(nullable: false),
                    NomeCatalogo = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Inicio = table.Column<DateTime>(nullable: false),
                    Termino = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoEntidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListaPrecoEntidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IntegrationCode = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    InsertedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    RemovedAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    LastUpdatedUserId = table.Column<Guid>(nullable: true),
                    Commited = table.Column<bool>(nullable: false),
                    Inicio = table.Column<DateTime>(nullable: false),
                    Termino = table.Column<DateTime>(nullable: false),
                    DescontoPercentual = table.Column<decimal>(nullable: false),
                    CatalogoEntidadeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaPrecoEntidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaPrecoEntidade_CatalogoEntidade_CatalogoEntidadeId",
                        column: x => x.CatalogoEntidadeId,
                        principalTable: "CatalogoEntidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IntegrationCode = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    InsertedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    RemovedAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    LastUpdatedUserId = table.Column<Guid>(nullable: true),
                    Commited = table.Column<bool>(nullable: false),
                    NomeProduto = table.Column<string>(nullable: true),
                    Perecivel = table.Column<bool>(nullable: false),
                    DataValidade = table.Column<DateTime>(nullable: false),
                    NumeroDeSerie = table.Column<string>(nullable: true),
                    CodigoDeBarras = table.Column<string>(type: "varchar(30)", nullable: true),
                    CodigoLote = table.Column<string>(type: "varchar(30)", nullable: true),
                    CatalogoEntidadeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoModel_CatalogoEntidade_CatalogoEntidadeId",
                        column: x => x.CatalogoEntidadeId,
                        principalTable: "CatalogoEntidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoListaPrecoModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IntegrationCode = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    InsertedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    RemovedAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    LastUpdatedUserId = table.Column<Guid>(nullable: true),
                    Commited = table.Column<bool>(nullable: false),
                    ProdutoId = table.Column<Guid>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    ListaPrecoEntidadeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoListaPrecoModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoListaPrecoModel_ListaPrecoEntidade_ListaPrecoEntidadeId",
                        column: x => x.ListaPrecoEntidadeId,
                        principalTable: "ListaPrecoEntidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdutoListaPrecoModel_ProdutoModel_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "ProdutoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoVariacoesEntidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IntegrationCode = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true),
                    InsertedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    RemovedAt = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    LastUpdatedUserId = table.Column<Guid>(nullable: true),
                    Commited = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    ProdutoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoVariacoesEntidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoVariacoesEntidade_ProdutoModel_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "ProdutoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoEntidade_IntegrationCode",
                table: "CatalogoEntidade",
                column: "IntegrationCode");

            migrationBuilder.CreateIndex(
                name: "IX_ListaPrecoEntidade_CatalogoEntidadeId",
                table: "ListaPrecoEntidade",
                column: "CatalogoEntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaPrecoEntidade_IntegrationCode",
                table: "ListaPrecoEntidade",
                column: "IntegrationCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoListaPrecoModel_IntegrationCode",
                table: "ProdutoListaPrecoModel",
                column: "IntegrationCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoListaPrecoModel_ListaPrecoEntidadeId",
                table: "ProdutoListaPrecoModel",
                column: "ListaPrecoEntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoListaPrecoModel_ProdutoId",
                table: "ProdutoListaPrecoModel",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoModel_CatalogoEntidadeId",
                table: "ProdutoModel",
                column: "CatalogoEntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoModel_IntegrationCode",
                table: "ProdutoModel",
                column: "IntegrationCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoVariacoesEntidade_IntegrationCode",
                table: "ProdutoVariacoesEntidade",
                column: "IntegrationCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoVariacoesEntidade_ProdutoId",
                table: "ProdutoVariacoesEntidade",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoListaPrecoModel");

            migrationBuilder.DropTable(
                name: "ProdutoVariacoesEntidade");

            migrationBuilder.DropTable(
                name: "ListaPrecoEntidade");

            migrationBuilder.DropTable(
                name: "ProdutoModel");

            migrationBuilder.DropTable(
                name: "CatalogoEntidade");
        }
    }
}
