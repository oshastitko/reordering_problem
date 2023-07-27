using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConsoleAppReorderingProblem.Migrations
{
    /// <inheritdoc />
    public partial class FillData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RegisteredDevice",
                columns: new[] { "Id", "DeviceName", "ModelNumber", "Position", "PreviousDeviceId" },
                values: new object[,]
                {
                    { 1, "J. J. Keller ELD - iOS 2.0", "20I", 0, null },
                    { 2, "J. J. Keller ELD - iOS 2.5", "25I", 1, 1 },
                    { 3, "#1 ELD by HOS247", "FLT3", 2, 2 },
                    { 4, "Xpert ELD", "N775G", 3, 3 },
                    { 5, "PeopleNet Mobile Gateway - Trimble Driver ELD", "PMG001", 4, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RegisteredDevice",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RegisteredDevice",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RegisteredDevice",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RegisteredDevice",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RegisteredDevice",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
