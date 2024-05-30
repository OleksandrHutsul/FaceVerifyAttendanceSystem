using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceVerifyAttendanceSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateUserApplicationDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminComment",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "NameDepartment",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "NameDepartment",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameDepartment",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AdminComment",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameDepartment",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
