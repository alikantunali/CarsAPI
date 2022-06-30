using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Common.Migrations
{
    /// <inheritdoc />
    public partial class CarDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandName", "ManufactureYear", "Model" },
                values: new object[,]
                {
                    { 1, "LAMBORGINI", "1998", "COUNTACH" },
                    { 2, "PORSCHE", "1976", "911 TURBO" },
                    { 3, "FORD", "1968", "MUSTANG" },
                    { 4, "HONDA", "2001", "CIVIC" },
                    { 5, "JEEP", "2019", "RUBICON" },
                    { 6, "SUBARU", "1999", "IMPREZA" },
                    { 7, "CHEVROLET", "2004", "CORVETTE" },
                    { 8, "FERRARI", "1997", "F40" },
                    { 9, "DODGE", "2013", "CHARGER" },
                    { 10, "MAZDA", "1998", "RX-3" },
                    { 11, "MERCEDES", "2010", "G-CLASS" },
                    { 12, "DODGE", "2002", "VIPER SRT" },
                    { 13, "TOYOTA", "1999", "Supra" },
                    { 14, "HONDA", "2002", "S2000" },
                    { 15, "BMW", "2022", "M5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
