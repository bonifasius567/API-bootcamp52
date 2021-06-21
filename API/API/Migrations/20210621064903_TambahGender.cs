using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class TambahGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "Tb_M_Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gender",
                table: "Tb_M_Employee");
        }
    }
}
