using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_Money_Transfer_MVC.Migrations
{
    public partial class Online : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoneyReceiver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverName = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyReceiver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneySender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderName = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneySender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderName = table.Column<string>(nullable: true),
                    ProviderWebURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneyTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferAmount = table.Column<decimal>(nullable: false),
                    TransferDateTime = table.Column<DateTime>(nullable: false),
                    MoneySenderId = table.Column<int>(nullable: false),
                    MoneyReceiverId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyTransfer_MoneyReceiver_MoneyReceiverId",
                        column: x => x.MoneyReceiverId,
                        principalTable: "MoneyReceiver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoneyTransfer_MoneySender_MoneySenderId",
                        column: x => x.MoneySenderId,
                        principalTable: "MoneySender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoneyTransfer_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransfer_MoneyReceiverId",
                table: "MoneyTransfer",
                column: "MoneyReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransfer_MoneySenderId",
                table: "MoneyTransfer",
                column: "MoneySenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransfer_ProviderId",
                table: "MoneyTransfer",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTransfer");

            migrationBuilder.DropTable(
                name: "MoneyReceiver");

            migrationBuilder.DropTable(
                name: "MoneySender");

            migrationBuilder.DropTable(
                name: "Provider");
        }
    }
}
