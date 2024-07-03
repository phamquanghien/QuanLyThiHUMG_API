using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThiHUMG.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_StudentExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentExams",
                columns: table => new
                {
                    StudentExamID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ClassName = table.Column<string>(type: "TEXT", nullable: true),
                    TestDay = table.Column<string>(type: "TEXT", nullable: true),
                    TestRoom = table.Column<string>(type: "TEXT", nullable: true),
                    LessonStart = table.Column<string>(type: "TEXT", nullable: false),
                    LessonNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ExamId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExamBag = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentCode = table.Column<string>(type: "TEXT", nullable: false),
                    SubjectCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExams", x => x.StudentExamID);
                    table.ForeignKey(
                        name: "FK_StudentExams_Students_StudentCode",
                        column: x => x.StudentCode,
                        principalTable: "Students",
                        principalColumn: "StudentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentExams_Subjects_SubjectCode",
                        column: x => x.SubjectCode,
                        principalTable: "Subjects",
                        principalColumn: "SubjectCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_StudentCode",
                table: "StudentExams",
                column: "StudentCode");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_SubjectCode",
                table: "StudentExams",
                column: "SubjectCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentExams");
        }
    }
}
