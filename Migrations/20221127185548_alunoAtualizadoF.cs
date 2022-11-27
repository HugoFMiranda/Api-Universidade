using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universidade_Api.Migrations
{
    public partial class alunoAtualizadoF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Saldo",
                table: "Alunos",
                type: "double",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Saldo",
                table: "Alunos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);
        }
    }
}
