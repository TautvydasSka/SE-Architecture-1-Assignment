using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ISBN = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    PageCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ISBN);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "ISBN", "Author", "PageCount", "Title" },
                values: new object[] { "9780132350884", "Robert C. Martin", 464, "Clean Code: A Handbook of Agile Software Craftsmanship" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "ISBN", "Author", "PageCount", "Title" },
                values: new object[] { "0134494164", "Robert C. Martin", 432, "Clean Architecture: A Craftsman's Guide to Software Structure and Design" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "ISBN", "Author", "PageCount", "Title" },
                values: new object[] { "0137081073", "Robert C. Martin", 242, "The Clean Coder: A Code of Conduct for Professional Programmers" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
