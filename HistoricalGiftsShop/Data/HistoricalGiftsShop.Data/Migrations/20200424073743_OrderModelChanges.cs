using Microsoft.EntityFrameworkCore.Migrations;

namespace HistoricalGiftsShop.Data.Migrations
{
    public partial class OrderModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourierOffice",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourierOffice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StatusType",
                table: "Orders");
        }
    }
}
