using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCliente.Migrations
{
    public partial class CreateIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato",
                table: "Contato");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaFisica",
                table: "PessoaFisica");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaJuridica",
                table: "PessoaJuridica");

            migrationBuilder.CreateIndex(
                name: "IX_CNPJ_Cliente",
                table: "PessoaJuridica",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nome_Cliente1",
                table: "PessoaJuridica",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CPF_Cliente",
                table: "PessoaFisica",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nome_Cliente",
                table: "PessoaFisica",
                column: "Nome",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Cliente",
                table: "Contato",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Cliente",
                table: "Endereco",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaFisica_Cliente",
                table: "PessoaFisica",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaJuridica_Cliente",
                table: "PessoaJuridica",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Cliente",
                table: "Contato");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Cliente",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaFisica_Cliente",
                table: "PessoaFisica");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaJuridica_Cliente",
                table: "PessoaJuridica");

            migrationBuilder.DropIndex(
                name: "IX_CNPJ_Cliente",
                table: "PessoaJuridica");

            migrationBuilder.DropIndex(
                name: "IX_Nome_Cliente1",
                table: "PessoaJuridica");

            migrationBuilder.DropIndex(
                name: "IX_CPF_Cliente",
                table: "PessoaFisica");

            migrationBuilder.DropIndex(
                name: "IX_Nome_Cliente",
                table: "PessoaFisica");

            migrationBuilder.AddForeignKey(
                name: "FK_Contato",
                table: "Contato",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco",
                table: "Endereco",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaFisica",
                table: "PessoaFisica",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaJuridica",
                table: "PessoaJuridica",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
