using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMT_Back.Migrations
{
    public partial class AddFriendAltKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Friend_RequestedById",
                table: "Friend");

            migrationBuilder.AddUniqueConstraint(
                name: "FriendUniqueAltKey",
                table: "Friend",
                columns: new[] { "RequestedById", "RequestedToId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "FriendUniqueAltKey",
                table: "Friend");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_RequestedById",
                table: "Friend",
                column: "RequestedById");
        }
    }
}
