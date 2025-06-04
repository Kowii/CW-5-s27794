using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apbd3.Migrations
{
    /// <inheritdoc />
    public partial class update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "Birthdate", "FirstName", "LastName" },
                values: new object[] { 2, new DateTime(1899, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marian", "Paździoch" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2);
        }
    }
}
