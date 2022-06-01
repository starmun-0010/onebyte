using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneByte.Migrations
{
    public partial class DoctorPatientRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorID",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DoctorID",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Doctors");

            migrationBuilder.CreateTable(
                name: "DoctorPatient",
                columns: table => new
                {
                    DoctorsID = table.Column<string>(type: "text", nullable: false),
                    PatientsID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatient", x => new { x.DoctorsID, x.PatientsID });
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Doctors_DoctorsID",
                        column: x => x.DoctorsID,
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Patients_PatientsID",
                        column: x => x.PatientsID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatient_PatientsID",
                table: "DoctorPatient",
                column: "PatientsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorPatient");

            migrationBuilder.AddColumn<string>(
                name: "DoctorID",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Doctors",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorID",
                table: "Patients",
                column: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Doctors_DoctorID",
                table: "Patients",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "ID");
        }
    }
}
