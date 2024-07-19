using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheypoorChi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class userDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8578b89d-ab89-47ed-a446-9e777a8acf56"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6691f168-627c-47b0-b992-692ba025485d"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("53c358c2-600e-4b7a-b4b2-629012ca7324"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("553ff22c-fafe-4e5c-8807-12459b180407"), true, new Guid("53c358c2-600e-4b7a-b4b2-629012ca7324"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("553ff22c-fafe-4e5c-8807-12459b180407"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("53c358c2-600e-4b7a-b4b2-629012ca7324"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("6691f168-627c-47b0-b992-692ba025485d"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("8578b89d-ab89-47ed-a446-9e777a8acf56"), true, new Guid("6691f168-627c-47b0-b992-692ba025485d"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });
        }
    }
}
