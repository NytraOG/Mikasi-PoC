using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Data.Migrations
{
    /// <inheritdoc />
    public partial class Mietstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "blackshield",
                table: "nutzungseinheiten",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                schema: "blackshield",
                table: "nutzungseinheiten");
        }
    }
}
