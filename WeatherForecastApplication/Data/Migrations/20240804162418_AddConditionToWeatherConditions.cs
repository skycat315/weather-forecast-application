using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecastApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConditionToWeatherConditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Condition",
                table: "WeatherConditions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "WeatherConditions");
        }
    }
}
