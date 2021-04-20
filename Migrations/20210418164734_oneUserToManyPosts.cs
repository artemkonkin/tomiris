using Microsoft.EntityFrameworkCore.Migrations;

namespace tomiris.Migrations
{
    public partial class oneUserToManyPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_UserId",
                table: "BlogPosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_UserId",
                table: "BlogPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_UserId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_UserId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BlogPosts");
        }
    }
}
