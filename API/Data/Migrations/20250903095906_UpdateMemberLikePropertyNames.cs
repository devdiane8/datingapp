using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMemberLikePropertyNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Members_sourceMemberId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Members_targetMemberId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "targetMemberId",
                table: "Likes",
                newName: "TargetMemberId");

            migrationBuilder.RenameColumn(
                name: "sourceMemberId",
                table: "Likes",
                newName: "SourceMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_targetMemberId",
                table: "Likes",
                newName: "IX_Likes_TargetMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Members_SourceMemberId",
                table: "Likes",
                column: "SourceMemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Members_TargetMemberId",
                table: "Likes",
                column: "TargetMemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Members_SourceMemberId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Members_TargetMemberId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "TargetMemberId",
                table: "Likes",
                newName: "targetMemberId");

            migrationBuilder.RenameColumn(
                name: "SourceMemberId",
                table: "Likes",
                newName: "sourceMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_TargetMemberId",
                table: "Likes",
                newName: "IX_Likes_targetMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Members_sourceMemberId",
                table: "Likes",
                column: "sourceMemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Members_targetMemberId",
                table: "Likes",
                column: "targetMemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
