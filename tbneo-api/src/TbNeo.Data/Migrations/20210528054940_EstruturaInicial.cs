using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TbNeo.Data.Migrations
{
    public partial class EstruturaInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Senha = table.Column<string>(type: "varchar(100)", nullable: false),
                    IdLogReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogSistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCampo = table.Column<string>(type: "varchar(100)", nullable: false),
                    ValorNovo = table.Column<string>(type: "varchar(100)", nullable: true),
                    ValorAntigo = table.Column<string>(type: "varchar(100)", nullable: true),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdAlteradoPor = table.Column<int>(type: "int", nullable: false),
                    IdLogReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSistema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogSistema_Usuario_IdAlteradoPor",
                        column: x => x.IdAlteradoPor,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: true),
                    UrlJira = table.Column<string>(type: "varchar(200)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdCriadoPor = table.Column<int>(type: "int", nullable: false),
                    IdAtualizadoPor = table.Column<int>(type: "int", nullable: true),
                    IdLogReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projeto_Usuario_IdAtualizadoPor",
                        column: x => x.IdAtualizadoPor,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projeto_Usuario_IdCriadoPor",
                        column: x => x.IdCriadoPor,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureFlag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    CriadoPorId = table.Column<int>(type: "int", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtualizadoPorId = table.Column<int>(type: "int", nullable: true),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdProjeto = table.Column<int>(type: "int", nullable: false),
                    IdCriadoPor = table.Column<int>(type: "int", nullable: false),
                    IdAtualizadoPor = table.Column<int>(type: "int", nullable: true),
                    IdLogReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFlag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureFlag_Projeto_IdProjeto",
                        column: x => x.IdProjeto,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureFlag_Usuario_AtualizadoPorId",
                        column: x => x.AtualizadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeatureFlag_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFlag_AtualizadoPorId",
                table: "FeatureFlag",
                column: "AtualizadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFlag_CriadoPorId",
                table: "FeatureFlag",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFlag_IdProjeto",
                table: "FeatureFlag",
                column: "IdProjeto");

            migrationBuilder.CreateIndex(
                name: "IX_LogSistema_IdAlteradoPor",
                table: "LogSistema",
                column: "IdAlteradoPor");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_IdAtualizadoPor",
                table: "Projeto",
                column: "IdAtualizadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_IdCriadoPor",
                table: "Projeto",
                column: "IdCriadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureFlag");

            migrationBuilder.DropTable(
                name: "LogSistema");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
