using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCliente.Migrations
{
    public partial class UserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bairo",
                table: "Endereco",
                newName: "Bairro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Endereco",
                newName: "Bairo");
        }
    }
}
