using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSAppFor_BincomIntermediate.Migrations
{
    /// <inheritdoc />
    public partial class AdminRerunMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LibraryUsers",
                keyColumn: "Id",
                keyValue: new Guid("dd6e2fe1-67a7-40bb-8ad8-bf21a600b581"));

            migrationBuilder.InsertData(
                table: "LibraryUsers",
                columns: new[] { "Id", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "Role" },
                values: new object[] { new Guid("cba1ce8e-ebe1-452d-b920-35b33d577769"), "bincom@gmail.com", "Bincom", false, "DevCenter", "$2a$11$QIOj5jvCedaZx335lJIPZ.LEQY5GYbA06jzN/TRPRF6Tei8uUYvsW", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LibraryUsers",
                keyColumn: "Id",
                keyValue: new Guid("cba1ce8e-ebe1-452d-b920-35b33d577769"));

            migrationBuilder.InsertData(
                table: "LibraryUsers",
                columns: new[] { "Id", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "Role" },
                values: new object[] { new Guid("dd6e2fe1-67a7-40bb-8ad8-bf21a600b581"), "admin@lms.com", "System", false, "Admin", "$2a$11$xJI8btXcgRMY89DF5R4LE.fexg9WEGEhQRMndR6/zxxlkm6dsVMo6", "Admin" });
        }
    }
}
