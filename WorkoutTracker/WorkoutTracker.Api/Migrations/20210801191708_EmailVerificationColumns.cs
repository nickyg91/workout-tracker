using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutTracker.Api.Migrations
{
    public partial class EmailVerificationColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_email_validated",
                schema: "workout",
                table: "workout_user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "validation_token",
                schema: "workout",
                table: "workout_user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_email_validated",
                schema: "workout",
                table: "workout_user");

            migrationBuilder.DropColumn(
                name: "validation_token",
                schema: "workout",
                table: "workout_user");
        }
    }
}
