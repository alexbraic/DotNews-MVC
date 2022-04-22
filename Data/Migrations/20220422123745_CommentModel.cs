using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNews.Data.Migrations
{
    public partial class CommentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    commentBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reportId = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
