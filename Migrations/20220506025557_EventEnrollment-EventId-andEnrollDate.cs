using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LEPS.Migrations
{
    public partial class EventEnrollmentEventIdandEnrollDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEnrollments_Series_SeriesId",
                table: "EventEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Series_SeriesId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_EventEnrollments_SeriesId",
                table: "EventEnrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "EventEnrollments");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_Events_SeriesId",
                table: "Event",
                newName: "IX_Event_SeriesId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollDate",
                table: "EventEnrollments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "EventEnrollments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EventEnrollments_EventId",
                table: "EventEnrollments",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Series_SeriesId",
                table: "Event",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventEnrollments_Event_EventId",
                table: "EventEnrollments",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Series_SeriesId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventEnrollments_Event_EventId",
                table: "EventEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_EventEnrollments_EventId",
                table: "EventEnrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EnrollDate",
                table: "EventEnrollments");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "EventEnrollments");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_Event_SeriesId",
                table: "Events",
                newName: "IX_Events_SeriesId");

            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "EventEnrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EventEnrollments_SeriesId",
                table: "EventEnrollments",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEnrollments_Series_SeriesId",
                table: "EventEnrollments",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Series_SeriesId",
                table: "Events",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
