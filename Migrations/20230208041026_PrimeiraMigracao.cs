using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NOVAteste.Migrations
{
    public partial class PrimeiraMigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arquivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),                 
                    Dados = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IV_Arquivo_Descricao",
                table: "Arquivo",
                column: "Descricao",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivo");
        }
    }
}
