using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientEngTranscription.DataAccess.Migrations
{
    public partial class Medication_followup_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "Medication",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicationFollowUps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TakenDate = table.Column<DateTime>(nullable: false),
                    TakenTime = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    MedicationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationFollowUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicationFollowUps_Medication_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationFollowUps_MedicationId",
                table: "MedicationFollowUps",
                column: "MedicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationFollowUps");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "Medication");
        }
    }
}
