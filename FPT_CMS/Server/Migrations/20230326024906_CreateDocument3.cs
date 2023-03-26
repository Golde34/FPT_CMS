using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class CreateDocument3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Accounts_AccountId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Courses_CourseId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFile_Document_DocumentationId",
                table: "DocumentFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentFile",
                table: "DocumentFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "DocumentFile",
                newName: "DocumentFiles");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFile_DocumentationId",
                table: "DocumentFiles",
                newName: "IX_DocumentFiles_DocumentationId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_CourseId",
                table: "Documents",
                newName: "IX_Documents_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_AccountId",
                table: "Documents",
                newName: "IX_Documents_AccountId");

            migrationBuilder.AddColumn<int>(
                name: "FileType",
                table: "DocumentFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentFiles",
                table: "DocumentFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFiles_Documents_DocumentationId",
                table: "DocumentFiles",
                column: "DocumentationId",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Accounts_AccountId",
                table: "Documents",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Courses_CourseId",
                table: "Documents",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFiles_Documents_DocumentationId",
                table: "DocumentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Accounts_AccountId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Courses_CourseId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentFiles",
                table: "DocumentFiles");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "DocumentFiles");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Document");

            migrationBuilder.RenameTable(
                name: "DocumentFiles",
                newName: "DocumentFile");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_CourseId",
                table: "Document",
                newName: "IX_Document_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_AccountId",
                table: "Document",
                newName: "IX_Document_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFiles_DocumentationId",
                table: "DocumentFile",
                newName: "IX_DocumentFile_DocumentationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "DocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentFile",
                table: "DocumentFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Accounts_AccountId",
                table: "Document",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Courses_CourseId",
                table: "Document",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFile_Document_DocumentationId",
                table: "DocumentFile",
                column: "DocumentationId",
                principalTable: "Document",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
