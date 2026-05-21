using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyecto2Back.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnsDuplicadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_AsignadoIdUsuario",
                table: "Tareas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_CreadorIdUsuario",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_AsignadoIdUsuario",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_CreadorIdUsuario",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "AsignadoIdUsuario",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "CreadorIdUsuario",
                table: "Tareas");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdAsignado",
                table: "Tareas",
                column: "IdAsignado");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdCreador",
                table: "Tareas",
                column: "IdCreador");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_IdAsignado",
                table: "Tareas",
                column: "IdAsignado",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_IdCreador",
                table: "Tareas",
                column: "IdCreador",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_IdAsignado",
                table: "Tareas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_IdCreador",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_IdAsignado",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_IdCreador",
                table: "Tareas");

            migrationBuilder.AddColumn<int>(
                name: "AsignadoIdUsuario",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreadorIdUsuario",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_AsignadoIdUsuario",
                table: "Tareas",
                column: "AsignadoIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_CreadorIdUsuario",
                table: "Tareas",
                column: "CreadorIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_AsignadoIdUsuario",
                table: "Tareas",
                column: "AsignadoIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_CreadorIdUsuario",
                table: "Tareas",
                column: "CreadorIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
