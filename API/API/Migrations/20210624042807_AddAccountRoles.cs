using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddAccountRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Tb_M_Employee",
                newName: "Gender");

            migrationBuilder.CreateTable(
                name: "Tb_M_Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_AccountRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_AccountRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_T_AccountRole_Tb_M_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Tb_M_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_T_AccountRole_Tb_T_Account_AccountNIK",
                        column: x => x.AccountNIK,
                        principalTable: "Tb_T_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_AccountRole_AccountNIK",
                table: "Tb_T_AccountRole",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_AccountRole_RoleID",
                table: "Tb_T_AccountRole",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_T_AccountRole");

            migrationBuilder.DropTable(
                name: "Tb_M_Role");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Tb_M_Employee",
                newName: "gender");
        }
    }
}
