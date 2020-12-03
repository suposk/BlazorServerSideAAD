using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Entities.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecomendedAction = table.Column<int>(nullable: false),
                    VersionValue = table.Column<int>(nullable: false),
                    VersionFull = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    DetailsFormat = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ReleasedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVersions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppVersions",
                columns: new[] { "Id", "CreatedAt", "Details", "DetailsFormat", "Link", "RecomendedAction", "ReleasedAt", "VersionFull", "VersionValue" },
                values: new object[] { 20, new DateTime(2020, 11, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), "<p>This is version 20</p>", "html", "www.bing.com", 1, new DateTime(2020, 11, 22, 16, 0, 0, 0, DateTimeKind.Unspecified), "1.0.20.0", 20 });

            migrationBuilder.InsertData(
                table: "AppVersions",
                columns: new[] { "Id", "CreatedAt", "Details", "DetailsFormat", "Link", "RecomendedAction", "ReleasedAt", "VersionFull", "VersionValue" },
                values: new object[] { 21, new DateTime(2020, 11, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), "This is version 21, modified at 5:00 pm", "text", "www.google.sk", 2, new DateTime(2020, 11, 25, 17, 0, 0, 0, DateTimeKind.Unspecified), "1.0.21.0", 21 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppVersions");
        }
    }
}
