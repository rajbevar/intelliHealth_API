using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientEngTranscription.DataAccess.Migrations
{
    public partial class addedpatientmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Medication",
                newName: "Strength");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zipcode",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isIgnored",
                table: "Notes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isParsed",
                table: "Notes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Frenquency",
                table: "Medication",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Zipcode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "isIgnored",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "isParsed",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Frenquency",
                table: "Medication");

            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Medication",
                newName: "Duration");
        }
    }
}
