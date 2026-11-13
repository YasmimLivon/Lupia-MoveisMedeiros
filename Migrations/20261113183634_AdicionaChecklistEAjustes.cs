using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoveisMedeiros.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaChecklistEAjustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Endereço",
                table: "Clientes",
                newName: "Endereco");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Checklists",
                newName: "ItensObraId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Prazo",
                table: "Ocorrencias",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<bool>(
                name: "Resolvida",
                table: "Ocorrencias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ArquitetoId",
                table: "Obras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Montadores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Foto",
                table: "ItensObras",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "ItensObras",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Checklists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Fotos",
                table: "Checklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Checklists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Arquitetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquitetos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResolucaoOcorrencias_MontadorId",
                table: "ResolucaoOcorrencias",
                column: "MontadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolucaoOcorrencias_OcorrenciaId",
                table: "ResolucaoOcorrencias",
                column: "OcorrenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_MontadorId",
                table: "Ocorrencias",
                column: "MontadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_ObraId",
                table: "Ocorrencias",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Obras_ArquitetoId",
                table: "Obras",
                column: "ArquitetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Obras_ClienteId",
                table: "Obras",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Obras_MontadorId",
                table: "Obras",
                column: "MontadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensObras_ObraId",
                table: "ItensObras",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_ItensObraId",
                table: "Checklists",
                column: "ItensObraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklists_ItensObras_ItensObraId",
                table: "Checklists",
                column: "ItensObraId",
                principalTable: "ItensObras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensObras_Obras_ObraId",
                table: "ItensObras",
                column: "ObraId",
                principalTable: "Obras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Obras_Arquitetos_ArquitetoId",
                table: "Obras",
                column: "ArquitetoId",
                principalTable: "Arquitetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Obras_Clientes_ClienteId",
                table: "Obras",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Obras_Montadores_MontadorId",
                table: "Obras",
                column: "MontadorId",
                principalTable: "Montadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencias_Montadores_MontadorId",
                table: "Ocorrencias",
                column: "MontadorId",
                principalTable: "Montadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencias_Obras_ObraId",
                table: "Ocorrencias",
                column: "ObraId",
                principalTable: "Obras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResolucaoOcorrencias_Montadores_MontadorId",
                table: "ResolucaoOcorrencias",
                column: "MontadorId",
                principalTable: "Montadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResolucaoOcorrencias_Ocorrencias_OcorrenciaId",
                table: "ResolucaoOcorrencias",
                column: "OcorrenciaId",
                principalTable: "Ocorrencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklists_ItensObras_ItensObraId",
                table: "Checklists");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensObras_Obras_ObraId",
                table: "ItensObras");

            migrationBuilder.DropForeignKey(
                name: "FK_Obras_Arquitetos_ArquitetoId",
                table: "Obras");

            migrationBuilder.DropForeignKey(
                name: "FK_Obras_Clientes_ClienteId",
                table: "Obras");

            migrationBuilder.DropForeignKey(
                name: "FK_Obras_Montadores_MontadorId",
                table: "Obras");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocorrencias_Montadores_MontadorId",
                table: "Ocorrencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocorrencias_Obras_ObraId",
                table: "Ocorrencias");

            migrationBuilder.DropForeignKey(
                name: "FK_ResolucaoOcorrencias_Montadores_MontadorId",
                table: "ResolucaoOcorrencias");

            migrationBuilder.DropForeignKey(
                name: "FK_ResolucaoOcorrencias_Ocorrencias_OcorrenciaId",
                table: "ResolucaoOcorrencias");

            migrationBuilder.DropTable(
                name: "Arquitetos");

            migrationBuilder.DropIndex(
                name: "IX_ResolucaoOcorrencias_MontadorId",
                table: "ResolucaoOcorrencias");

            migrationBuilder.DropIndex(
                name: "IX_ResolucaoOcorrencias_OcorrenciaId",
                table: "ResolucaoOcorrencias");

            migrationBuilder.DropIndex(
                name: "IX_Ocorrencias_MontadorId",
                table: "Ocorrencias");

            migrationBuilder.DropIndex(
                name: "IX_Ocorrencias_ObraId",
                table: "Ocorrencias");

            migrationBuilder.DropIndex(
                name: "IX_Obras_ArquitetoId",
                table: "Obras");

            migrationBuilder.DropIndex(
                name: "IX_Obras_ClienteId",
                table: "Obras");

            migrationBuilder.DropIndex(
                name: "IX_Obras_MontadorId",
                table: "Obras");

            migrationBuilder.DropIndex(
                name: "IX_ItensObras_ObraId",
                table: "ItensObras");

            migrationBuilder.DropIndex(
                name: "IX_Checklists_ItensObraId",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "Prazo",
                table: "Ocorrencias");

            migrationBuilder.DropColumn(
                name: "Resolvida",
                table: "Ocorrencias");

            migrationBuilder.DropColumn(
                name: "ArquitetoId",
                table: "Obras");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Fotos",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Checklists");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "Clientes",
                newName: "Endereço");

            migrationBuilder.RenameColumn(
                name: "ItensObraId",
                table: "Checklists",
                newName: "ItemId");

            migrationBuilder.AlterColumn<int>(
                name: "Telefone",
                table: "Montadores",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Foto",
                table: "ItensObras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "ItensObras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Telefone",
                table: "Clientes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Checklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
