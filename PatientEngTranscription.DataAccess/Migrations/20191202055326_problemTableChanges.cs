using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientEngTranscription.DataAccess.Migrations
{
    public partial class problemTableChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsProblem",
                table: "Problems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsProblem",
                table: "Problems",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
