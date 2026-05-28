using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameDokumentToDokumente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_dokument_nutzungseinheiten_nutzungseinheit_id",
                schema: "blackshield",
                table: "dokument");

            migrationBuilder.DropPrimaryKey(
                name: "pk_dokument",
                schema: "blackshield",
                table: "dokument");

            migrationBuilder.RenameTable(
                name: "dokument",
                schema: "blackshield",
                newName: "dokumente",
                newSchema: "blackshield");

            migrationBuilder.RenameIndex(
                name: "ix_dokument_nutzungseinheit_id",
                schema: "blackshield",
                table: "dokumente",
                newName: "ix_dokumente_nutzungseinheit_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_dokumente",
                schema: "blackshield",
                table: "dokumente",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_dokumente_nutzungseinheiten_nutzungseinheit_id",
                schema: "blackshield",
                table: "dokumente",
                column: "nutzungseinheit_id",
                principalSchema: "blackshield",
                principalTable: "nutzungseinheiten",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_dokumente_nutzungseinheiten_nutzungseinheit_id",
                schema: "blackshield",
                table: "dokumente");

            migrationBuilder.DropPrimaryKey(
                name: "pk_dokumente",
                schema: "blackshield",
                table: "dokumente");

            migrationBuilder.RenameTable(
                name: "dokumente",
                schema: "blackshield",
                newName: "dokument",
                newSchema: "blackshield");

            migrationBuilder.RenameIndex(
                name: "ix_dokumente_nutzungseinheit_id",
                schema: "blackshield",
                table: "dokument",
                newName: "ix_dokument_nutzungseinheit_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_dokument",
                schema: "blackshield",
                table: "dokument",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_dokument_nutzungseinheiten_nutzungseinheit_id",
                schema: "blackshield",
                table: "dokument",
                column: "nutzungseinheit_id",
                principalSchema: "blackshield",
                principalTable: "nutzungseinheiten",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
