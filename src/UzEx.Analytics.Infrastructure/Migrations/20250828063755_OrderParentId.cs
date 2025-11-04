using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UzEx.Analytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderParentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "parent_id",
                table: "orders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "orders");
        }
    }
}
