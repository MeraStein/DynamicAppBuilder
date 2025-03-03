using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicAppBuilder.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPropIdToControlProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropsId",
                table: "ControlProperties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControlProperties_PropsId",
                table: "ControlProperties",
                column: "PropsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlProperties_Props_PropsId",
                table: "ControlProperties",
                column: "PropsId",
                principalTable: "Props",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlProperties_Props_PropsId",
                table: "ControlProperties");

            migrationBuilder.DropIndex(
                name: "IX_ControlProperties_PropsId",
                table: "ControlProperties");

            migrationBuilder.DropColumn(
                name: "PropsId",
                table: "ControlProperties");
        }
    }
}
