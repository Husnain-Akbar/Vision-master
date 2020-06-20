using Microsoft.EntityFrameworkCore.Migrations;

namespace Vision.Data.Migrations
{
    public partial class alters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Quotes_QuoteId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteId",
                table: "Books",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Quotes_QuoteId",
                table: "Books",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Quotes_QuoteId",
                table: "Books",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
