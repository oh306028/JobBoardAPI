using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoardAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JobOfferts",
                columns: new[] { "Id", "CompanyName", "Description", "JobTime", "JobType", "Location", "Salary", "Title" },
                values: new object[] { 1, "TrustFormulaIt", "Job offer for C# developer with minimum 3 years experience and graduated", 1, 1, "Warsaw", 2500.0, "Software developer" });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "Age", "Education", "Experience", "JobOffertId" },
                values: new object[] { 1, 20, 0, 3, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobOfferts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
