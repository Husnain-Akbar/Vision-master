using Microsoft.EntityFrameworkCore.Migrations;

namespace Vision.Data.Migrations
{
    public partial class alter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Books_BookId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_BookId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Quotes");

            migrationBuilder.AddColumn<int>(
                name: "QuoteId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_QuoteId",
                table: "Books",
                column: "QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Quotes_QuoteId",
                table: "Books",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Quotes_QuoteId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_QuoteId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Quotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_BookId",
                table: "Quotes",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Books_BookId",
                table: "Quotes",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
