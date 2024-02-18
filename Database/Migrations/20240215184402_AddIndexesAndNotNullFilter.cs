using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesAndNotNullFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Documents_Directory",
                table: "Documents",
                column: "Directory",
                unique: true,
                filter: "[Directory] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Name",
                table: "Documents",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_Directory",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_Name",
                table: "Documents");
        }
    }
}
