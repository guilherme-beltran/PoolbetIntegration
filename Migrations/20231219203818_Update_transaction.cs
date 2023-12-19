using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolbetIntegration.API.Migrations
{
    /// <inheritdoc />
    public partial class Update_transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetId",
                table: "Transaction");

            migrationBuilder.AddColumn<string>(
                name: "BetUuiId",
                table: "Transaction",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetUuiId",
                table: "Transaction");

            migrationBuilder.AddColumn<string>(
                name: "BetId",
                table: "Transaction",
                type: "string",
                nullable: false,
                defaultValue: "");
        }
    }
}
