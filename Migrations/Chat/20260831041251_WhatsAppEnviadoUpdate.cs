using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoSegundoParcialPrueba1.Migrations.Chat
{
    /// <inheritdoc />
    public partial class WhatsAppEnviadoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WhatsAppEnviados",
                table: "WhatsAppEnviados");

            migrationBuilder.RenameTable(
                name: "WhatsAppEnviados",
                newName: "WhatsAppsEnviados");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "WhatsAppsEnviados",
                newName: "NumeroDestino");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "WhatsAppsEnviados",
                newName: "FechaEnvio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhatsAppsEnviados",
                table: "WhatsAppsEnviados",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WhatsAppsEnviados",
                table: "WhatsAppsEnviados");

            migrationBuilder.RenameTable(
                name: "WhatsAppsEnviados",
                newName: "WhatsAppEnviados");

            migrationBuilder.RenameColumn(
                name: "NumeroDestino",
                table: "WhatsAppEnviados",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "FechaEnvio",
                table: "WhatsAppEnviados",
                newName: "Fecha");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhatsAppEnviados",
                table: "WhatsAppEnviados",
                column: "Id");
        }
    }
}
