using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicAppBuilder.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToControlProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlProperties_Props_PropsId",
                table: "ControlProperties");

            migrationBuilder.AlterColumn<int>(
                name: "PropsId",
                table: "ControlProperties",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlProperties_Props_PropsId",
                table: "ControlProperties",
                column: "PropsId",
                principalTable: "Props",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlProperties_Props_PropsId",
                table: "ControlProperties");

            migrationBuilder.AlterColumn<int>(
                name: "PropsId",
                table: "ControlProperties",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlProperties_Props_PropsId",
                table: "ControlProperties",
                column: "PropsId",
                principalTable: "Props",
                principalColumn: "Id");
        }
    }
}
