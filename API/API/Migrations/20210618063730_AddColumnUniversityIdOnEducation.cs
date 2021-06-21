using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddColumnUniversityIdOnEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                table: "Tb_M_Education");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Tb_M_Education",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                table: "Tb_M_Education",
                column: "UniversityId",
                principalTable: "Tb_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                table: "Tb_M_Education");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Tb_M_Education",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                table: "Tb_M_Education",
                column: "UniversityId",
                principalTable: "Tb_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
