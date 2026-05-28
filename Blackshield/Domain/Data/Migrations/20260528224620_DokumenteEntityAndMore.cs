using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Data.Migrations
{
    /// <inheritdoc />
    public partial class DokumenteEntityAndMore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dokument",
                schema: "blackshield",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nutzungseinheit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dateiname = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    content_type = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    größe_bytes = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    updated_by = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dokument", x => x.id);
                    table.ForeignKey(
                        name: "fk_dokument_nutzungseinheiten_nutzungseinheit_id",
                        column: x => x.nutzungseinheit_id,
                        principalSchema: "blackshield",
                        principalTable: "nutzungseinheiten",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_dokument_nutzungseinheit_id",
                schema: "blackshield",
                table: "dokument",
                column: "nutzungseinheit_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dokument",
                schema: "blackshield");
        }
    }
}
