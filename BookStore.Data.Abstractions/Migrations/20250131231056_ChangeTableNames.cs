using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Abstractions.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDto_CategoryDto_CategoryId",
                table: "BookDto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleDto_RoleDto_RolesId",
                table: "UserRoleDto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleDto_UserDto_UsersId",
                table: "UserRoleDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDto",
                table: "UserDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleDto",
                table: "RoleDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryDto",
                table: "CategoryDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookDto",
                table: "BookDto");

            migrationBuilder.RenameTable(
                name: "UserDto",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "RoleDto",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "CategoryDto",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "BookDto",
                newName: "Books");

            migrationBuilder.RenameIndex(
                name: "IX_BookDto_CategoryId",
                table: "Books",
                newName: "IX_Books_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleDto_Roles_RolesId",
                table: "UserRoleDto",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleDto_Users_UsersId",
                table: "UserRoleDto",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleDto_Roles_RolesId",
                table: "UserRoleDto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleDto_Users_UsersId",
                table: "UserRoleDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserDto");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "RoleDto");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "CategoryDto");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "BookDto");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryId",
                table: "BookDto",
                newName: "IX_BookDto_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDto",
                table: "UserDto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleDto",
                table: "RoleDto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryDto",
                table: "CategoryDto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookDto",
                table: "BookDto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDto_CategoryDto_CategoryId",
                table: "BookDto",
                column: "CategoryId",
                principalTable: "CategoryDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleDto_RoleDto_RolesId",
                table: "UserRoleDto",
                column: "RolesId",
                principalTable: "RoleDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleDto_UserDto_UsersId",
                table: "UserRoleDto",
                column: "UsersId",
                principalTable: "UserDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
