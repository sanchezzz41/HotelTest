using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HotelTest.Web.Migrations
{
    public partial class AddVisitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_TypeRooms_RoomOptions",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomOptions",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomOptions",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "Rooms",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoomOptionId",
                table: "Rooms",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DateOfDeparture = table.Column<DateTime>(type: "timestamp", nullable: false),
                    RoomId = table.Column<uint>(type: "oid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitors_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomOptionId",
                table: "Rooms",
                column: "RoomOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_RoomId",
                table: "Visitors",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_UserId",
                table: "Visitors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_TypeRooms_RoomOptionId",
                table: "Rooms",
                column: "RoomOptionId",
                principalTable: "TypeRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_TypeRooms_RoomOptionId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomOptionId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomOptionId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "RoomOptions",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomOptions",
                table: "Rooms",
                column: "RoomOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_TypeRooms_RoomOptions",
                table: "Rooms",
                column: "RoomOptions",
                principalTable: "TypeRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
