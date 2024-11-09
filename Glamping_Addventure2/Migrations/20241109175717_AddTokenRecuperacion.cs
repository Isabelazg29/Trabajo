using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glamping_Addventure2.Migrations
{
    public partial class AddTokenRecuperacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TokenRecuperacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    FechaExpiracion = table.Column<DateTime>(nullable: false),
                    Usado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenRecuperacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenRecuperacion_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Idusuario",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenRecuperacion");
        }
    }
}
