using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GSMWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrivacyPolicyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrivacyPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyName1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description1 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PolicyName2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description2 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PolicyName3 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description3 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PolicyName4 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description4 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PolicyName5 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description5 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivacyPolicies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivacyPolicies");
        }
    }
}
