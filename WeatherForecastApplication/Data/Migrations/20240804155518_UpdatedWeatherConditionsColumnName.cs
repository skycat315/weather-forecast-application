using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecastApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedWeatherConditionsColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "WeatherConditions",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "WeatherConditions",
                newName: "DateTime");
        }
    }
}
