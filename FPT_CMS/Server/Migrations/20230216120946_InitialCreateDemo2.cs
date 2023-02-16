using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class InitialCreateDemo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurriculumDetails_Subjects_SubjectCode1",
                table: "CurriculumDetails");

            migrationBuilder.DropIndex(
                name: "IX_CurriculumDetails_SubjectCode1",
                table: "CurriculumDetails");

            migrationBuilder.DropColumn(
                name: "SubjectCode1",
                table: "CurriculumDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubjectCode1",
                table: "CurriculumDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumDetails_SubjectCode1",
                table: "CurriculumDetails",
                column: "SubjectCode1");

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculumDetails_Subjects_SubjectCode1",
                table: "CurriculumDetails",
                column: "SubjectCode1",
                principalTable: "Subjects",
                principalColumn: "SubjectCode");
        }
    }
}
