using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyCrm.Core.Migrations
{
    public partial class updateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LastGross",
                schema: "core",
                table: "Customer",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGross",
                schema: "core",
                table: "Customer",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastGross",
                schema: "core",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "TotalGross",
                schema: "core",
                table: "Customer");
        }
    }
}
