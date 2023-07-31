using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityUserManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditIdentityModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_Roles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Users_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_Users_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "IdentityUserTokens",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "IdentityUserLogins",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "IdentityRoleClaims",
                newSchema: "security");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "security",
                table: "IdentityUserLogins",
                newName: "IX_IdentityUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "security",
                table: "IdentityRoleClaims",
                newName: "IX_IdentityRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserTokens",
                schema: "security",
                table: "IdentityUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserLogins",
                schema: "security",
                table: "IdentityUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityRoleClaims",
                schema: "security",
                table: "IdentityRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaims_Roles_RoleId",
                schema: "security",
                table: "IdentityRoleClaims",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogins_Users_UserId",
                schema: "security",
                table: "IdentityUserLogins",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserTokens_Users_UserId",
                schema: "security",
                table: "IdentityUserTokens",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRoleClaims_Roles_RoleId",
                schema: "security",
                table: "IdentityRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserLogins_Users_UserId",
                schema: "security",
                table: "IdentityUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserTokens_Users_UserId",
                schema: "security",
                table: "IdentityUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserTokens",
                schema: "security",
                table: "IdentityUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserLogins",
                schema: "security",
                table: "IdentityUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityRoleClaims",
                schema: "security",
                table: "IdentityRoleClaims");

            migrationBuilder.RenameTable(
                name: "IdentityUserTokens",
                schema: "security",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "IdentityUserLogins",
                schema: "security",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "IdentityRoleClaims",
                schema: "security",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Roles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Users_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Users_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
