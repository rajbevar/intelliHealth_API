using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientEngTranscription.DataAccess.Migrations
{
    public partial class problemcategoryadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "category",
                table: "Problems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Problems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Problems");
        }
    }
}
