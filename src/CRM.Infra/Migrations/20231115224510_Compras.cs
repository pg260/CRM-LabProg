using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Compras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorHistorico",
                table: "HistoricoCompras",
                newName: "ValorTotal");

            migrationBuilder.AddColumn<string>(
                name: "MetodoDePagameto",
                table: "HistoricoCompras",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetodoDePagameto",
                table: "HistoricoCompras");

            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "HistoricoCompras",
                newName: "ValorHistorico");
        }
    }
}
