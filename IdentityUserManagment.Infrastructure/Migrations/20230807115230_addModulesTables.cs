using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityUserManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addModulesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "modules");

            migrationBuilder.CreateTable(
                name: "Modules",
                schema: "modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleSections",
                schema: "modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleSections_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalSchema: "modules",
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                schema: "modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ModuleSectionId = table.Column<int>(type: "int", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_ModuleSections_ModuleSectionId",
                        column: x => x.ModuleSectionId,
                        principalSchema: "modules",
                        principalTable: "ModuleSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageActions",
                schema: "modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageActions_Pages_PageId",
                        column: x => x.PageId,
                        principalSchema: "modules",
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleSections_ModuleId",
                schema: "modules",
                table: "ModuleSections",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_PageActions_PageId",
                schema: "modules",
                table: "PageActions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ModuleSectionId",
                schema: "modules",
                table: "Pages",
                column: "ModuleSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageActions",
                schema: "modules");

            migrationBuilder.DropTable(
                name: "Pages",
                schema: "modules");

            migrationBuilder.DropTable(
                name: "ModuleSections",
                schema: "modules");

            migrationBuilder.DropTable(
                name: "Modules",
                schema: "modules");
        }
    }
}
