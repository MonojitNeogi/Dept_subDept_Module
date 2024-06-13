using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dept_subDept_Module.Migrations
{
    /// <inheritdoc />
    public partial class update_reminder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipientEmail",
                table: "Reminder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientEmail",
                table: "Reminder");
        }
    }
}
