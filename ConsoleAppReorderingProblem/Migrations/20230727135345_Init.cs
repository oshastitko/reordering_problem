using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleAppReorderingProblem.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegisteredDevice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreviousDeviceId = table.Column<int>(type: "int", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredDevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisteredDevice_RegisteredDevice_PreviousDeviceId",
                        column: x => x.PreviousDeviceId,
                        principalTable: "RegisteredDevice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredDevice_PreviousDeviceId",
                table: "RegisteredDevice",
                column: "PreviousDeviceId",
                unique: true,
                filter: "[PreviousDeviceId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisteredDevice");
        }
    }
}
