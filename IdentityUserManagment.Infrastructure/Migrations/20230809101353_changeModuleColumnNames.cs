using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityUserManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeModuleColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameEn",
                schema: "modules",
                table: "Pages",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                schema: "modules",
                table: "Pages",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                schema: "modules",
                table: "PageActions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                schema: "modules",
                table: "PageActions",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                schema: "modules",
                table: "ModuleSections",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                schema: "modules",
                table: "ModuleSections",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                schema: "modules",
                table: "Modules",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                schema: "modules",
                table: "Modules",
                newName: "Name_ar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name_ar",
                schema: "modules",
                table: "Pages",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "modules",
                table: "Pages",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                schema: "modules",
                table: "PageActions",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "modules",
                table: "PageActions",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                schema: "modules",
                table: "ModuleSections",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "modules",
                table: "ModuleSections",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                schema: "modules",
                table: "Modules",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "modules",
                table: "Modules",
                newName: "NameEn");
        }
    }
}
