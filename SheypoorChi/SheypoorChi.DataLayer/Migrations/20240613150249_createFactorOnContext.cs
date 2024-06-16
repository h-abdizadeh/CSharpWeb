using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SheypoorChi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class createFactorOnContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factor_Users_UserId",
                table: "Factor");

            migrationBuilder.DropForeignKey(
                name: "FK_FactorDetail_Factor_FactorId",
                table: "FactorDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_FactorDetail_Products_ProductId",
                table: "FactorDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FactorDetail",
                table: "FactorDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factor",
                table: "Factor");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aa7dd91e-586f-46c4-afc1-d3b19d4f3295"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2c9187ec-6ab6-4808-9750-a6410283f224"));

            migrationBuilder.RenameTable(
                name: "FactorDetail",
                newName: "FactorDetails");

            migrationBuilder.RenameTable(
                name: "Factor",
                newName: "Factors");

            migrationBuilder.RenameIndex(
                name: "IX_FactorDetail_ProductId",
                table: "FactorDetails",
                newName: "IX_FactorDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_FactorDetail_FactorId",
                table: "FactorDetails",
                newName: "IX_FactorDetails_FactorId");

            migrationBuilder.RenameIndex(
                name: "IX_Factor_UserId",
                table: "Factors",
                newName: "IX_Factors_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FactorDetails",
                table: "FactorDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factors",
                table: "Factors",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("dc47fc98-6845-4bfd-8656-00ce7d059e64"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("dcc71b3b-34b1-483b-93b6-5b295d1817e2"), true, new Guid("dc47fc98-6845-4bfd-8656-00ce7d059e64"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_FactorDetails_Factors_FactorId",
                table: "FactorDetails",
                column: "FactorId",
                principalTable: "Factors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_FactorDetails_Products_ProductId",
                table: "FactorDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Factors_Users_UserId",
                table: "Factors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactorDetails_Factors_FactorId",
                table: "FactorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FactorDetails_Products_ProductId",
                table: "FactorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Factors_Users_UserId",
                table: "Factors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Factors",
                table: "Factors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FactorDetails",
                table: "FactorDetails");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dcc71b3b-34b1-483b-93b6-5b295d1817e2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dc47fc98-6845-4bfd-8656-00ce7d059e64"));

            migrationBuilder.RenameTable(
                name: "Factors",
                newName: "Factor");

            migrationBuilder.RenameTable(
                name: "FactorDetails",
                newName: "FactorDetail");

            migrationBuilder.RenameIndex(
                name: "IX_Factors_UserId",
                table: "Factor",
                newName: "IX_Factor_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FactorDetails_ProductId",
                table: "FactorDetail",
                newName: "IX_FactorDetail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_FactorDetails_FactorId",
                table: "FactorDetail",
                newName: "IX_FactorDetail_FactorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Factor",
                table: "Factor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FactorDetail",
                table: "FactorDetail",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { new Guid("2c9187ec-6ab6-4808-9750-a6410283f224"), "admin", "مدیر" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "RoleId", "UserName", "UserPassword" },
                values: new object[] { new Guid("aa7dd91e-586f-46c4-afc1-d3b19d4f3295"), true, new Guid("2c9187ec-6ab6-4808-9750-a6410283f224"), "09112223344", "JfnnlDI7RTiF9RgfG2JNCw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Factor_Users_UserId",
                table: "Factor",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactorDetail_Factor_FactorId",
                table: "FactorDetail",
                column: "FactorId",
                principalTable: "Factor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactorDetail_Products_ProductId",
                table: "FactorDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
