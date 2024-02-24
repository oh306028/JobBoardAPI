using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoardAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByUserSeekerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Seekers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Seekers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seekers_CreatedById",
                table: "Seekers",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Seekers_Users_CreatedById",
                table: "Seekers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seekers_Users_CreatedById",
                table: "Seekers");

            migrationBuilder.DropIndex(
                name: "IX_Seekers_CreatedById",
                table: "Seekers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Seekers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Seekers");
        }
    }
}
