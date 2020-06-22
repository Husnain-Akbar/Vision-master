using Microsoft.EntityFrameworkCore.Migrations;

namespace Vision.Data.Migrations
{
    public partial class alteration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Quotes_QuoteId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteId",
                table: "Books",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "QuoteId",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Quotes_QuoteId",
                table: "Books",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
