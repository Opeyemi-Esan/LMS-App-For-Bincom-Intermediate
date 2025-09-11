using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSAppFor_BincomIntermediate.Migrations
{
    /// <inheritdoc />
    public partial class AdminMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "LibraryUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "LibraryUsers",
                columns: new[] { "Id", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "Role" },
                values: new object[] { new Guid("dd6e2fe1-67a7-40bb-8ad8-bf21a600b581"), "admin@lms.com", "System", false, "Admin", "$2a$11$xJI8btXcgRMY89DF5R4LE.fexg9WEGEhQRMndR6/zxxlkm6dsVMo6", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LibraryUsers",
                keyColumn: "Id",
                keyValue: new Guid("dd6e2fe1-67a7-40bb-8ad8-bf21a600b581"));

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "LibraryUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
