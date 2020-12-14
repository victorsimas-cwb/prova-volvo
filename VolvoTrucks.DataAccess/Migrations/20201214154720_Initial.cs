using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoTrucks.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "VOLVOTRUCKS");

            migrationBuilder.CreateTable(
                name: "TRUCK_MODELS",
                schema: "VOLVOTRUCKS",
                columns: table => new
                {
                    TruckModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRUCK_MODELS", x => x.TruckModelId);
                });

            migrationBuilder.CreateTable(
                name: "TRUCKS",
                schema: "VOLVOTRUCKS",
                columns: table => new
                {
                    TruckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckModelId = table.Column<int>(type: "int", nullable: false),
                    ManufacturingYear = table.Column<int>(type: "int", nullable: false),
                    ModelYear = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRUCKS", x => x.TruckId);
                    table.ForeignKey(
                        name: "FK_TRUCKS_TRUCK_MODELS_TruckModelId",
                        column: x => x.TruckModelId,
                        principalSchema: "VOLVOTRUCKS",
                        principalTable: "TRUCK_MODELS",
                        principalColumn: "TruckModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "VOLVOTRUCKS",
                table: "TRUCK_MODELS",
                columns: new[] { "TruckModelId", "Available", "Model" },
                values: new object[,]
                {
                    { 1, true, "FH" },
                    { 2, true, "FM" },
                    { 3, false, "FMX" },
                    { 4, false, "VM" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRUCKS_TruckModelId",
                schema: "VOLVOTRUCKS",
                table: "TRUCKS",
                column: "TruckModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRUCKS",
                schema: "VOLVOTRUCKS");

            migrationBuilder.DropTable(
                name: "TRUCK_MODELS",
                schema: "VOLVOTRUCKS");
        }
    }
}
