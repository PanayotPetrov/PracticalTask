#nullable disable

namespace PracticalTask.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedGuidFileModelEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuidFileModelId",
                table: "GuidModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GuidFileModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuidFileModels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuidModels_GuidFileModelId",
                table: "GuidModels",
                column: "GuidFileModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuidModels_GuidFileModels_GuidFileModelId",
                table: "GuidModels",
                column: "GuidFileModelId",
                principalTable: "GuidFileModels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuidModels_GuidFileModels_GuidFileModelId",
                table: "GuidModels");

            migrationBuilder.DropTable(
                name: "GuidFileModels");

            migrationBuilder.DropIndex(
                name: "IX_GuidModels_GuidFileModelId",
                table: "GuidModels");

            migrationBuilder.DropColumn(
                name: "GuidFileModelId",
                table: "GuidModels");
        }
    }
}
