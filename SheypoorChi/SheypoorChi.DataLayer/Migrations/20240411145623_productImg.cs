using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheypoorChi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class productImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a25e2b1f-c791-42f2-a208-f6fffcbff1d0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2d3139f7-af19-4c32-8686-808bc60494e0"));

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("19e81c35-b8a7-47d8-afc4-89fbd5e02de2"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("7155ccbf-9dab-4112-bbac-9680b89bc802"), true, new Guid("19e81c35-b8a7-47d8-afc4-89fbd5e02de2"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7155ccbf-9dab-4112-bbac-9680b89bc802"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("19e81c35-b8a7-47d8-afc4-89fbd5e02de2"));

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("2d3139f7-af19-4c32-8686-808bc60494e0"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("a25e2b1f-c791-42f2-a208-f6fffcbff1d0"), true, new Guid("2d3139f7-af19-4c32-8686-808bc60494e0"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });
        }
    }
}
