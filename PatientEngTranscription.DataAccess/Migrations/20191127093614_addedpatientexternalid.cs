using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientEngTranscription.DataAccess.Migrations
{
    public partial class addedpatientexternalid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Patients");
        }
    }
}
