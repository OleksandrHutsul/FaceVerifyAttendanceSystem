using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceVerifyAttendanceSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class deleteUpdateUserApplicationDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameDepartment",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "NameDepartment",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameDepartment",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "NameDepartment",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
