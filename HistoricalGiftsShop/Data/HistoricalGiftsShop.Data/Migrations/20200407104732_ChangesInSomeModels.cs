using Microsoft.EntityFrameworkCore.Migrations;

namespace HistoricalGiftsShop.Data.Migrations
{
    public partial class ChangesInSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_BookId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Paintings",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CeramicId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Paintings");

            migrationBuilder.AlterColumn<int>(
                name: "CeramicId",
                table: "Images",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_BookId",
                table: "Images",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
