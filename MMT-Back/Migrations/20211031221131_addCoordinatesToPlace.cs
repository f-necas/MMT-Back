using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace MMT_Back.Migrations
{
    public partial class addCoordinatesToPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Coordinate",
                table: "Place",
                type: "geography",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Invitation",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Place",
                keyColumn: "Id",
                keyValue: 1,
                column: "Coordinate",
                value: (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (45.57528871439694 5.9587356460298855)"));

            migrationBuilder.UpdateData(
                table: "Place",
                keyColumn: "Id",
                keyValue: 2,
                column: "Coordinate",
                value: (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (45.57510642776163 5.949668211603892)"));

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_UserId",
                table: "Invitation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Users_UserId",
                table: "Invitation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Users_UserId",
                table: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Invitation_UserId",
                table: "Invitation");

            migrationBuilder.DropColumn(
                name: "Coordinate",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Invitation");
        }
    }
}
