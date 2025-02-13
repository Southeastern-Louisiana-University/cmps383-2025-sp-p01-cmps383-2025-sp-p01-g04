using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Selu383.SP25.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixTheaterConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Theaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Theaters",
                columns: new[] { "Id", "Address", "Name", "SeatCount" },
                values: new object[,]
                {
                    { 1, "123 W University Ave", "Lions Den Grande Theater", 200 },
                    { 2, "456 Palace Dr", "Lions Den Mega Theater", 150 },
                    { 3, "789 S Range Rd", "Lions Den Green and Gold Theater", 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Theaters");
        }
    }
}
