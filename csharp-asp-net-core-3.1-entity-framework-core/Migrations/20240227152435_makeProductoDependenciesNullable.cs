using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class makeProductoDependenciesNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_TipoAplicacion_TipoAplicacion",
                table: "Producto");

            migrationBuilder.AlterColumn<int>(
                name: "TipoAplicacion",
                table: "Producto",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_TipoAplicacion_TipoAplicacion",
                table: "Producto",
                column: "TipoAplicacion",
                principalTable: "TipoAplicacion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_TipoAplicacion_TipoAplicacion",
                table: "Producto");

            migrationBuilder.AlterColumn<int>(
                name: "TipoAplicacion",
                table: "Producto",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_TipoAplicacion_TipoAplicacion",
                table: "Producto",
                column: "TipoAplicacion",
                principalTable: "TipoAplicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
