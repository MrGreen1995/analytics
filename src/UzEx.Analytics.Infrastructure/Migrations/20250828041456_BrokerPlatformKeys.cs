using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UzEx.Analytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BrokerPlatformKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "new_spot_key",
                table: "brokers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "old_spot_key",
                table: "brokers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "new_spot_key",
                table: "brokers");

            migrationBuilder.DropColumn(
                name: "old_spot_key",
                table: "brokers");
        }
    }
}
