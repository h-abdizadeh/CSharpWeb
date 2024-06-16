using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheypoorChi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class createFactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f0976fa7-3d8e-4a78-a1e6-32d9a280e6c7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9fd0b3c0-d8ba-4b94-b234-35a47cd8791f"));

            migrationBuilder.CreateTable(
                name: "Factor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    PayDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factor_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DetailCount = table.Column<int>(type: "int", nullable: false),
                    DetailPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorDetail_Factor_FactorId",
                        column: x => x.FactorId,
                        principalTable: "Factor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FactorDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("2c9187ec-6ab6-4808-9750-a6410283f224"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("aa7dd91e-586f-46c4-afc1-d3b19d4f3295"), true, new Guid("2c9187ec-6ab6-4808-9750-a6410283f224"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Factor_UserId",
                table: "Factor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorDetail_FactorId",
                table: "FactorDetail",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorDetail_ProductId",
                table: "FactorDetail",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactorDetail");

            migrationBuilder.DropTable(
                name: "Factor");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aa7dd91e-586f-46c4-afc1-d3b19d4f3295"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2c9187ec-6ab6-4808-9750-a6410283f224"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("9fd0b3c0-d8ba-4b94-b234-35a47cd8791f"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("f0976fa7-3d8e-4a78-a1e6-32d9a280e6c7"), true, new Guid("9fd0b3c0-d8ba-4b94-b234-35a47cd8791f"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });
        }
    }
}
