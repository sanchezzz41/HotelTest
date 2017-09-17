using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HotelTest.Web.Migrations
{
    public partial class EditRoomId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Visitors",
                type: "int4",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "MaxCount",
                table: "Rooms",
                type: "int4",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Rooms",
                type: "int4",
                nullable: false,
                oldClrType: typeof(uint));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "RoomId",
                table: "Visitors",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<uint>(
                name: "MaxCount",
                table: "Rooms",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "Rooms",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");
        }
    }
}
