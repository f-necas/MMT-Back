using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMT_Back.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedById = table.Column<int>(type: "int", nullable: false),
                    RequestedToId = table.Column<int>(type: "int", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BecameFriendsTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FriendRequestFlag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friend_Users_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friend_Users_RequestedToId",
                        column: x => x.RequestedToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequesterUserId = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventPlaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEvents_Place_EventPlaceId",
                        column: x => x.EventPlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEvents_Users_RequesterUserId",
                        column: x => x.RequesterUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEventId = table.Column<int>(type: "int", nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvitedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_UserEvents_UserEventId",
                        column: x => x.UserEventId,
                        principalTable: "UserEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitation_Users_InvitedUserId",
                        column: x => x.InvitedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Place",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "340 Chem. des Carrières, 73230 Saint-Alban-Leysse", "Wattabloc" },
                    { 2, "95 Rue de Bolliet, 73230 Saint-Alban-Leysse", "Archimalt" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "UserName" },
                values: new object[,]
                {
                    { 1, "Flo" },
                    { 2, "Angie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friend_RequestedById",
                table: "Friend",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_RequestedToId",
                table: "Friend",
                column: "RequestedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_InvitedUserId",
                table: "Invitation",
                column: "InvitedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_UserEventId",
                table: "Invitation",
                column: "UserEventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_EventPlaceId",
                table: "UserEvents",
                column: "EventPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_RequesterUserId",
                table: "UserEvents",
                column: "RequesterUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "UserEvents");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
