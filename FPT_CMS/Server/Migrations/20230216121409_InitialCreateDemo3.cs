using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class InitialCreateDemo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CurriculumDetails_SubjectCode",
                table: "CurriculumDetails",
                column: "SubjectCode");

            migrationBuilder.AddForeignKey(
                name: "FK_CurriculumDetail_Subject",
                table: "CurriculumDetails",
                column: "SubjectCode",
                principalTable: "Subjects",
                principalColumn: "SubjectCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurriculumDetail_Subject",
                table: "CurriculumDetails");

            migrationBuilder.DropIndex(
                name: "IX_CurriculumDetails_SubjectCode",
                table: "CurriculumDetails");
        }
    }
}
