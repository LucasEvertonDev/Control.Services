using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Includeaudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    TableName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: true),
                    NewValues = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: true),
                    AffectedColumns = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: true),
                    PrimaryKey = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");
        }
    }
}
