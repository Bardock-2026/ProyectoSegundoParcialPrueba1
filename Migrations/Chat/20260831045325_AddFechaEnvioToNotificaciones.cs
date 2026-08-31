using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoSegundoParcialPrueba1.Migrations.Chat
{
    /// <inheritdoc />
    public partial class AddFechaEnvioToNotificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "CorreosEnviados",
                newName: "FechaEnvio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaEnvio",
                table: "CorreosEnviados",
                newName: "Fecha");
        }
    }
}
