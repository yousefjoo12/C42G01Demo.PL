using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Data.Migrations
{
    public partial class RelationshipBetweenDepartmentAndEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "departmentid",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_departmentid",
                table: "Employees",
                column: "departmentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_departmentid",
                table: "Employees",
                column: "departmentid",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_departmentid",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_departmentid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "departmentid",
                table: "Employees");
        }
    }
}
