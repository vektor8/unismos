using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unismos.Data.Migrations
{
    public partial class taxDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Taxes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Taxes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Taxes");
        }
    }
}
