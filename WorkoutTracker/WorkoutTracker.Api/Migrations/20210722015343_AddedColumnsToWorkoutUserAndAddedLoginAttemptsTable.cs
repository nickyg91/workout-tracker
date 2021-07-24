using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WorkoutTracker.Api.Migrations
{
    public partial class AddedColumnsToWorkoutUserAndAddedLoginAttemptsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "account_deactivated",
                schema: "workout",
                table: "workout_user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "user_name",
                schema: "workout",
                table: "workout_user",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "login_attempts",
                schema: "workout",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    last_login_attempt_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_successful = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_attempts", x => x.id);
                    table.ForeignKey(
                        name: "FK_login_attempts_workout_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "workout",
                        principalTable: "workout_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_login_attempts_user_id",
                schema: "workout",
                table: "login_attempts",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login_attempts",
                schema: "workout");

            migrationBuilder.DropColumn(
                name: "account_deactivated",
                schema: "workout",
                table: "workout_user");

            migrationBuilder.DropColumn(
                name: "user_name",
                schema: "workout",
                table: "workout_user");
        }
    }
}
