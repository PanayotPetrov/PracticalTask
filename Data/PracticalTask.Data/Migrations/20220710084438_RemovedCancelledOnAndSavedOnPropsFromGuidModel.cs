#nullable disable

namespace PracticalTask.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemovedCancelledOnAndSavedOnPropsFromGuidModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelledOn",
                table: "GuidModels");

            migrationBuilder.DropColumn(
                name: "ReadyToBeSavedOn",
                table: "GuidModels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledOn",
                table: "GuidModels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadyToBeSavedOn",
                table: "GuidModels",
                type: "datetime2",
                nullable: true);
        }
    }
}
