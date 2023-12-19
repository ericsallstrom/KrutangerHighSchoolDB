using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KrutangerHighSchoolDB.Migrations
{
    /// <inheritdoc />
    public partial class AddLatestMonthGradesView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassList",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassList", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "SeeCourses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genders__4E24E9F7FF3B3015", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "GradeType",
                columns: table => new
                {
                    GradeId = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeType", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    JobTitleId = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.JobTitleId);
                });

            migrationBuilder.CreateTable(
                name: "SeeStudents",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    SSN = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    FK_GenderId = table.Column<byte>(type: "tinyint", nullable: true),
                    PhoneNr = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    Email = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true),
                    FK_ClassId = table.Column<int>(type: "int", nullable: true),
                    SchoolStart = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_ClassList",
                        column: x => x.FK_ClassId,
                        principalTable: "ClassList",
                        principalColumn: "ClassId");
                    table.ForeignKey(
                        name: "FK_Students_Genders",
                        column: x => x.FK_GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderChoice");
                });

            migrationBuilder.CreateTable(
                name: "SeePersonnel",
                columns: table => new
                {
                    PersonnelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    SSN = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    FK_JobTitleId = table.Column<byte>(type: "tinyint", nullable: true),
                    FK_GenderId = table.Column<byte>(type: "tinyint", nullable: true),
                    PhoneNr = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    Email = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.PersonnelId);
                    table.ForeignKey(
                        name: "FK_Personnel_Genders",
                        column: x => x.FK_GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderChoice");
                    table.ForeignKey(
                        name: "FK_Personnel_JobTitles",
                        column: x => x.FK_JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "JobTitleId");
                });

            migrationBuilder.CreateTable(
                name: "CourseRegistration",
                columns: table => new
                {
                    CourseRegId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_StudentId = table.Column<int>(type: "int", nullable: true),
                    FK_CourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRegistration", x => x.CourseRegId);
                    table.ForeignKey(
                        name: "FK_CourseRegistration_Courses",
                        column: x => x.FK_CourseId,
                        principalTable: "SeeCourses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_CourseRegistration_Students",
                        column: x => x.FK_StudentId,
                        principalTable: "SeeStudents",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "ClassMentor",
                columns: table => new
                {
                    FK_PersonnelId = table.Column<int>(type: "int", nullable: true),
                    FK_ClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ClassMentor_ClassList",
                        column: x => x.FK_ClassId,
                        principalTable: "ClassList",
                        principalColumn: "ClassId");
                    table.ForeignKey(
                        name: "FK_ClassMentor_Personnel",
                        column: x => x.FK_PersonnelId,
                        principalTable: "SeePersonnel",
                        principalColumn: "PersonnelId");
                });

            migrationBuilder.CreateTable(
                name: "CourseTeacher",
                columns: table => new
                {
                    CourseTeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_PersonnelId = table.Column<int>(type: "int", nullable: true),
                    FK_CourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTeacher", x => x.CourseTeacherId);
                    table.ForeignKey(
                        name: "FK_CourseTeacher_Courses",
                        column: x => x.FK_CourseId,
                        principalTable: "SeeCourses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_CourseTeacher_Personnel",
                        column: x => x.FK_PersonnelId,
                        principalTable: "SeePersonnel",
                        principalColumn: "PersonnelId");
                });

            migrationBuilder.CreateTable(
                name: "SeeGrades",
                columns: table => new
                {
                    GradedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_CourseRegId = table.Column<int>(type: "int", nullable: true),
                    FK_GradeId = table.Column<byte>(type: "tinyint", nullable: true),
                    GradedDate = table.Column<DateTime>(type: "date", nullable: true),
                    FK_CourseTeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradedId);
                    table.ForeignKey(
                        name: "FK_Grades_CourseRegistration",
                        column: x => x.FK_CourseRegId,
                        principalTable: "CourseRegistration",
                        principalColumn: "CourseRegId");
                    table.ForeignKey(
                        name: "FK_Grades_CourseTeacher",
                        column: x => x.FK_CourseTeacherId,
                        principalTable: "CourseTeacher",
                        principalColumn: "CourseTeacherId");
                    table.ForeignKey(
                        name: "FK_Grades_GradeType",
                        column: x => x.FK_GradeId,
                        principalTable: "GradeType",
                        principalColumn: "GradeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassMentor_FK_ClassId",
                table: "ClassMentor",
                column: "FK_ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassMentor_FK_PersonnelId",
                table: "ClassMentor",
                column: "FK_PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_FK_CourseId",
                table: "CourseRegistration",
                column: "FK_CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistration_FK_StudentId",
                table: "CourseRegistration",
                column: "FK_StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeacher_FK_CourseId",
                table: "CourseTeacher",
                column: "FK_CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeacher_FK_PersonnelId",
                table: "CourseTeacher",
                column: "FK_PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_CourseRegId",
                table: "SeeGrades",
                column: "FK_CourseRegId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_CourseTeacherId",
                table: "SeeGrades",
                column: "FK_CourseTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_GradeId",
                table: "SeeGrades",
                column: "FK_GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_FK_GenderId",
                table: "SeePersonnel",
                column: "FK_GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_FK_JobTitleId",
                table: "SeePersonnel",
                column: "FK_JobTitleId");

            migrationBuilder.CreateIndex(
                name: "UQ_Personnel_SSN",
                table: "SeePersonnel",
                column: "SSN",
                unique: true,
                filter: "[SSN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_PersonnelSSN",
                table: "SeePersonnel",
                column: "SSN",
                unique: true,
                filter: "[SSN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FK_ClassId",
                table: "SeeStudents",
                column: "FK_ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FK_GenderId",
                table: "SeeStudents",
                column: "FK_GenderId");

            migrationBuilder.CreateIndex(
                name: "UQ_Students_SSN",
                table: "SeeStudents",
                column: "SSN",
                unique: true,
                filter: "[SSN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_StudentSSN",
                table: "SeeStudents",
                column: "SSN",
                unique: true,
                filter: "[SSN] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassMentor");

            migrationBuilder.DropTable(
                name: "SeeGrades");

            migrationBuilder.DropTable(
                name: "CourseRegistration");

            migrationBuilder.DropTable(
                name: "CourseTeacher");

            migrationBuilder.DropTable(
                name: "GradeType");

            migrationBuilder.DropTable(
                name: "SeeStudents");

            migrationBuilder.DropTable(
                name: "SeeCourses");

            migrationBuilder.DropTable(
                name: "SeePersonnel");

            migrationBuilder.DropTable(
                name: "ClassList");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "JobTitles");
        }
    }
}
