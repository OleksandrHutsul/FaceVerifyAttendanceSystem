using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceVerifyAttendanceSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadPhoto",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadPhoto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
