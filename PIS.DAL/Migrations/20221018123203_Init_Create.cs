using Microsoft.EntityFrameworkCore.Migrations;

namespace PIS.DAL.Migrations
{
    public partial class Init_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobID);
                });

            migrationBuilder.CreateTable(
                name: "Workplace",
                columns: table => new
                {
                    WorkplaceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workplace", x => x.WorkplaceID);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    WorkerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    WorkplaceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.WorkerID);
                    table.ForeignKey(
                        name: "FK_Worker_Workplace_WorkplaceID",
                        column: x => x.WorkplaceID,
                        principalTable: "Workplace",
                        principalColumn: "WorkplaceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobWorker",
                columns: table => new
                {
                    JobsJobID = table.Column<int>(type: "int", nullable: false),
                    WorkersWorkerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobWorker", x => new { x.JobsJobID, x.WorkersWorkerID });
                    table.ForeignKey(
                        name: "FK_JobWorker_Job_JobsJobID",
                        column: x => x.JobsJobID,
                        principalTable: "Job",
                        principalColumn: "JobID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobWorker_Worker_WorkersWorkerID",
                        column: x => x.WorkersWorkerID,
                        principalTable: "Worker",
                        principalColumn: "WorkerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobWorker_WorkersWorkerID",
                table: "JobWorker",
                column: "WorkersWorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_WorkplaceID",
                table: "Worker",
                column: "WorkplaceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobWorker");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "Workplace");
        }
    }
}
