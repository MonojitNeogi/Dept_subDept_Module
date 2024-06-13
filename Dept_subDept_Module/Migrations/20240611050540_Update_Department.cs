using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dept_subDept_Module.Migrations
{
    /// <inheritdoc />
    public partial class Update_Department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Parent_Id",
                table: "Department",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parent_Id",
                table: "Department");
        }
    }
}
