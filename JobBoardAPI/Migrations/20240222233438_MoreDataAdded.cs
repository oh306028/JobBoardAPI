using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobBoardAPI.Migrations
{
    /// <inheritdoc />
    public partial class MoreDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JobOfferts",
                columns: new[] { "Id", "CompanyName", "Description", "JobTime", "JobType", "Location", "Salary", "Title" },
                values: new object[,]
                {
                    { 2, "TechCo", "Exciting opportunity for a skilled frontend developer", 0, 2, "New York", 5000.0, "Frontend Developer" },
                    { 3, "CodeNerds", "Join our team as a backend developer and work on cutting-edge projects", 0, 1, "San Francisco", 6000.0, "Backend Developer" }
                });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "Age", "Education", "Experience", "JobOffertId" },
                values: new object[,]
                {
                    { 2, 25, 2, 3, 2 },
                    { 3, 19, 1, 5, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JobOfferts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobOfferts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
