using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicAppBuilder.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Props",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Props", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ControlProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    coordinates_X = table.Column<double>(type: "REAL", nullable: false),
                    coordinates_Y = table.Column<double>(type: "REAL", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Placeholder = table.Column<string>(type: "TEXT", nullable: false),
                    defaultValue = table.Column<string>(type: "TEXT", nullable: false),
                    Options = table.Column<string>(type: "TEXT", nullable: false),
                    PropId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlProperties_Props_PropId",
                        column: x => x.PropId,
                        principalTable: "Props",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlProperties_PropId",
                table: "ControlProperties",
                column: "PropId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlProperties");

            migrationBuilder.DropTable(
                name: "Props");
        }
    }
}
