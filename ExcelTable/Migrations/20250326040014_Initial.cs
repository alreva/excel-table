using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExcelTable.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Progress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Person = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Position = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CompanyAndBoothNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LinkedIn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Source = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LeadStatus = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    AfterDiscussionNotes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
