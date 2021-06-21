using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RemoveGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Tb_M_Employee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Tb_M_Employee",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
