using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceVerifyAttendanceSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initSheduleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleDayOfWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDayOfWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleTimeId = table.Column<int>(type: "int", nullable: false),
                    ScheduleDayOfWeekId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_ScheduleDayOfWeeks_ScheduleDayOfWeekId",
                        column: x => x.ScheduleDayOfWeekId,
                        principalTable: "ScheduleDayOfWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_ScheduleTimes_ScheduleTimeId",
                        column: x => x.ScheduleTimeId,
                        principalTable: "ScheduleTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_LessonId",
                table: "Schedules",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ScheduleDayOfWeekId",
                table: "Schedules",
                column: "ScheduleDayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ScheduleTimeId",
                table: "Schedules",
                column: "ScheduleTimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "ScheduleDayOfWeeks");

            migrationBuilder.DropTable(
                name: "ScheduleTimes");
        }
    }
}
