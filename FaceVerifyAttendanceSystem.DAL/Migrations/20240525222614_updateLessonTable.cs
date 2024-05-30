using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceVerifyAttendanceSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateLessonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_AspNetUsers_UserId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_UserId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionCourse",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LessonUser",
                columns: table => new
                {
                    LessonsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUser", x => new { x.LessonsId, x.UserId });
                    table.ForeignKey(
                        name: "FK_LessonUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonUser_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonUser_UserId",
                table: "LessonUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonUser");

            migrationBuilder.DropColumn(
                name: "DescriptionCourse",
                table: "Lessons");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_UserId",
                table: "Lessons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_AspNetUsers_UserId",
                table: "Lessons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
