using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace kliniqQ.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patient",
                columns: table => new
                {
                    patient_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    national_id = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    full_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient", x => x.patient_id);
                });

            migrationBuilder.CreateTable(
                name: "station",
                columns: table => new
                {
                    station_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    station_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_station", x => x.station_id);
                });

            migrationBuilder.CreateTable(
                name: "nurse",
                columns: table => new
                {
                    nurse_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    employee_number = table.Column<string>(type: "text", nullable: false),
                    current_station_id = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nurse", x => x.nurse_id);
                    table.ForeignKey(
                        name: "FK_nurse_station_current_station_id",
                        column: x => x.current_station_id,
                        principalTable: "station",
                        principalColumn: "station_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    ticket_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticket_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    issued_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    called_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    service_start_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    service_end_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    issued_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    StationId = table.Column<int>(type: "integer", nullable: true),
                    AssignedNurseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.ticket_id);
                    table.CheckConstraint("valid_times", "(called_at IS NULL OR called_at >= issued_at) AND (service_start_at IS NULL OR service_start_at >= called_at) AND (service_end_at IS NULL OR service_end_at >= service_start_at)");
                    table.ForeignKey(
                        name: "FK_ticket_nurse_AssignedNurseId",
                        column: x => x.AssignedNurseId,
                        principalTable: "nurse",
                        principalColumn: "nurse_id");
                    table.ForeignKey(
                        name: "FK_ticket_patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patient",
                        principalColumn: "patient_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_station_StationId",
                        column: x => x.StationId,
                        principalTable: "station",
                        principalColumn: "station_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_nurse_current_station_id",
                table: "nurse",
                column: "current_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_national_id",
                table: "patient",
                column: "national_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_AssignedNurseId",
                table: "ticket",
                column: "AssignedNurseId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_PatientId_issued_date",
                table: "ticket",
                columns: new[] { "PatientId", "issued_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_StationId",
                table: "ticket",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_ticket_number_issued_date",
                table: "ticket",
                columns: new[] { "ticket_number", "issued_date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "nurse");

            migrationBuilder.DropTable(
                name: "patient");

            migrationBuilder.DropTable(
                name: "station");
        }
    }
}
