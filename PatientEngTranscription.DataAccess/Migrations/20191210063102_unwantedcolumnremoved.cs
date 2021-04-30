using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientEngTranscription.DataAccess.Migrations
{
    public partial class unwantedcolumnremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Problems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Problems",
                nullable: false,
                defaultValue: 0);
        }
    }
}
