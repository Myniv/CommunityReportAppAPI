using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixforeignkeypost_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_CommunityPosts_CommunityPostPostId",
                table: "Discussions");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_CommunityPostPostId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "CommunityPostPostId",
                table: "Discussions");

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_post_id",
                table: "Discussions",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_user_id",
                table: "Discussions",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_CommunityPosts_post_id",
                table: "Discussions",
                column: "post_id",
                principalTable: "CommunityPosts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Profiles_user_id",
                table: "Discussions",
                column: "user_id",
                principalTable: "Profiles",
                principalColumn: "uid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_CommunityPosts_post_id",
                table: "Discussions");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Profiles_user_id",
                table: "Discussions");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_post_id",
                table: "Discussions");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_user_id",
                table: "Discussions");

            migrationBuilder.AddColumn<int>(
                name: "CommunityPostPostId",
                table: "Discussions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_CommunityPostPostId",
                table: "Discussions",
                column: "CommunityPostPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_CommunityPosts_CommunityPostPostId",
                table: "Discussions",
                column: "CommunityPostPostId",
                principalTable: "CommunityPosts",
                principalColumn: "id");
        }
    }
}
