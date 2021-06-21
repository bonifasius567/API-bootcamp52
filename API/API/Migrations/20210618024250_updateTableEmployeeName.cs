using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateTableEmployeeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Tb_M_Employee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_M_Employee",
                table: "Tb_M_Employee",
                column: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_M_Employee",
                table: "Tb_M_Employee");

            migrationBuilder.RenameTable(
                name: "Tb_M_Employee",
                newName: "Employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "NIK");
        }
    }
}
