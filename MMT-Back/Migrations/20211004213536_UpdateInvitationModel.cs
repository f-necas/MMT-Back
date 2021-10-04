using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMT_Back.Migrations
{
    public partial class UpdateInvitationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Users_InvitedUserId",
                table: "Invitation");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Users_InvitedUserId",
                table: "Invitation",
                column: "InvitedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Users_InvitedUserId",
                table: "Invitation");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Users_InvitedUserId",
                table: "Invitation",
                column: "InvitedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
