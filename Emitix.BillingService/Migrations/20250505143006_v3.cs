using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emitix.BillingService.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Number_Series",
                table: "Invoice",
                columns: new[] { "Number", "Series" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoice_Number_Series",
                table: "Invoice");
        }
    }
}
