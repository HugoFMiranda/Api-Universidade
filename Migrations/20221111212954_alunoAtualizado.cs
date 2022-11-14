using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universidade_Api.Migrations
{
    public partial class alunoAtualizado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Alunos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "Saldo",
                table: "Alunos",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Alunos");
        }
    }
}
