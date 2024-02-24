using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoardAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByUserIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "JobOfferts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "JobOfferts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "JobOfferts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "JobOfferts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedById",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferts_CreatedById",
                table: "JobOfferts",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferts_Users_CreatedById",
                table: "JobOfferts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferts_Users_CreatedById",
                table: "JobOfferts");

            migrationBuilder.DropIndex(
                name: "IX_JobOfferts_CreatedById",
                table: "JobOfferts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "JobOfferts");
        }
    }
}
