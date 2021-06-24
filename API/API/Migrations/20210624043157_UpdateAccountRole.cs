using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UpdateAccountRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_T_Account_AccountNIK",
                table: "Tb_T_AccountRole");

            migrationBuilder.DropColumn(
                name: "NIK",
                table: "Tb_T_AccountRole");

            migrationBuilder.RenameColumn(
                name: "AccountNIK",
                table: "Tb_T_AccountRole",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Tb_T_AccountRole_AccountNIK",
                table: "Tb_T_AccountRole",
                newName: "IX_Tb_T_AccountRole_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_T_Account_AccountId",
                table: "Tb_T_AccountRole",
                column: "AccountId",
                principalTable: "Tb_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_T_Account_AccountId",
                table: "Tb_T_AccountRole");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Tb_T_AccountRole",
                newName: "AccountNIK");

            migrationBuilder.RenameIndex(
                name: "IX_Tb_T_AccountRole_AccountId",
                table: "Tb_T_AccountRole",
                newName: "IX_Tb_T_AccountRole_AccountNIK");

            migrationBuilder.AddColumn<string>(
                name: "NIK",
                table: "Tb_T_AccountRole",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_T_Account_AccountNIK",
                table: "Tb_T_AccountRole",
                column: "AccountNIK",
                principalTable: "Tb_T_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
