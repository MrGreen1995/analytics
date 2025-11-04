using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UzEx.Analytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brokers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    business_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    reg_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    number = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    region = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brokers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "calendars",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_year = table.Column<int>(type: "integer", nullable: false),
                    date_month = table.Column<int>(type: "integer", nullable: false),
                    date_day = table.Column<int>(type: "integer", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_key = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_calendars", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    reg_number = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    country = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    region = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    district = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    address = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    business_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: true),
                    number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    platform = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    form = table.Column<int>(type: "integer", nullable: false),
                    trade_type = table.Column<int>(type: "integer", nullable: false),
                    lot = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    base_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    delivery_base = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    warehouse = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    origin_country = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contracts", x => x.id);
                    table.ForeignKey(
                        name: "fk_contracts_product_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "deals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    business_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    calendar_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    seller_client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    seller_broker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    buyer_client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    buyer_broker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    number = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    session_type = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    payment_days = table.Column<int>(type: "integer", nullable: false),
                    delivery_days = table.Column<int>(type: "integer", nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    close_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    annul_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    annul_reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deals", x => x.id);
                    table.ForeignKey(
                        name: "fk_deals_brokers_buyer_broker_id",
                        column: x => x.buyer_broker_id,
                        principalTable: "brokers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_deals_brokers_seller_broker_id",
                        column: x => x.seller_broker_id,
                        principalTable: "brokers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_deals_calendars_calendar_id",
                        column: x => x.calendar_id,
                        principalTable: "calendars",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_deals_clients_buyer_client_id",
                        column: x => x.buyer_client_id,
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_deals_clients_seller_client_id",
                        column: x => x.seller_client_id,
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_deals_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    calendar_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    broker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    direction = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    receive_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_brokers_broker_id",
                        column: x => x.broker_id,
                        principalTable: "brokers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_calendars_calendar_id",
                        column: x => x.calendar_id,
                        principalTable: "calendars",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_calendars_date_key",
                table: "calendars",
                column: "date_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contracts_product_id",
                table: "contracts",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_deals_buyer_broker_id",
                table: "deals",
                column: "buyer_broker_id");

            migrationBuilder.CreateIndex(
                name: "ix_deals_buyer_client_id",
                table: "deals",
                column: "buyer_client_id");

            migrationBuilder.CreateIndex(
                name: "ix_deals_calendar_id",
                table: "deals",
                column: "calendar_id");

            migrationBuilder.CreateIndex(
                name: "ix_deals_contract_id",
                table: "deals",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_deals_seller_broker_id",
                table: "deals",
                column: "seller_broker_id");

            migrationBuilder.CreateIndex(
                name: "ix_deals_seller_client_id",
                table: "deals",
                column: "seller_client_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_broker_id",
                table: "orders",
                column: "broker_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_calendar_id",
                table: "orders",
                column: "calendar_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_client_id",
                table: "orders",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_contract_id",
                table: "orders",
                column: "contract_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deals");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "brokers");

            migrationBuilder.DropTable(
                name: "calendars");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
