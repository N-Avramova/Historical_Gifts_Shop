using Microsoft.EntityFrameworkCore.Migrations;

namespace HistoricalGiftsShop.Data.Migrations
{
    public partial class UpdateInitialModelsWithSomeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookCoverTypes_CoverTypeId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CoverTypeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CoverTypeId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Ceramics",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Measure",
                table: "Ceramics",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CeramicId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaintingId",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookCoverTypeId",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookCoverTypeId",
                table: "Books",
                column: "BookCoverTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookCoverTypes_BookCoverTypeId",
                table: "Books",
                column: "BookCoverTypeId",
                principalTable: "BookCoverTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookCoverTypes_BookCoverTypeId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookCoverTypeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Measure",
                table: "Ceramics");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CeramicId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PaintingId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BookCoverTypeId",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Capacity",
                table: "Ceramics",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CoverTypeId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CoverTypeId",
                table: "Books",
                column: "CoverTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookCoverTypes_CoverTypeId",
                table: "Books",
                column: "CoverTypeId",
                principalTable: "BookCoverTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
