using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UzEx.Analytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DealPledges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "buyer_clearing_commission_amount",
                table: "deals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "buyer_clearing_commission_currency",
                table: "deals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "buyer_pledge_amount",
                table: "deals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "buyer_pledge_currency",
                table: "deals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "buyer_trade_commission_amount",
                table: "deals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "buyer_trade_commission_currency",
                table: "deals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "seller_clearing_commission_amount",
                table: "deals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "seller_clearing_commission_currency",
                table: "deals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "seller_pledge_amount",
                table: "deals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "seller_pledge_currency",
                table: "deals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "seller_trade_commission_amount",
                table: "deals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "seller_trade_commission_currency",
                table: "deals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "buyer_clearing_commission_amount",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "buyer_clearing_commission_currency",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "buyer_pledge_amount",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "buyer_pledge_currency",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "buyer_trade_commission_amount",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "buyer_trade_commission_currency",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "seller_clearing_commission_amount",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "seller_clearing_commission_currency",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "seller_pledge_amount",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "seller_pledge_currency",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "seller_trade_commission_amount",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "seller_trade_commission_currency",
                table: "deals");
        }
    }
}
