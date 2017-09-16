using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HotelTest.Web.Migrations
{
    public partial class AddTypeRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeRoom",
                table: "Rooms");

            migrationBuilder.CreateTable(
                name: "TypeRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false),
                    NameOfTypeRoom = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeRooms", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_TypeRooms_RoomOptions",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "TypeRooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomOptions",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "TypeRoom",
                table: "Rooms",
                nullable: true);
        }
    }
}
