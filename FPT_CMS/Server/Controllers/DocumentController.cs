using Microsoft.AspNetCore.Mvc;
using Server.DAO;
using Server.Entity;
using Server.Entity.Enum;
using Server.Repository.@interface;
using Server.Repository;

namespace Server.Controllers
{
    public class DocumentController : Controller
    {
		private readonly IWebHostEnvironment _env;

		public DocumentController(IWebHostEnvironment env)
		{
			_env = env;
		}
		public IActionResult GetDocumentsByCourseId(string courseId)
        {
			var webRootPath = _env.WebRootPath;
			if (!Directory.Exists(webRootPath + "\\Document\\"))
			{
				Directory.CreateDirectory(webRootPath + "\\Document\\");
			}
			DocumentManagement docManage = new DocumentManagement();
            List<Document> documents = docManage.GetDocumentsByCourseId(courseId);
            return Ok(documents);
        }

        public IActionResult GetDocumentFilesByDocumentId(int documentId)
        {
            DocumentFileManagement filesManage = new DocumentFileManagement();
            List<DocumentFile> files = filesManage.GetDocumentFilesByDocumentId(documentId);
            return Ok(files);
        }

        [HttpPost]
        public async Task<IActionResult> AddDocument([FromForm] string AccountId, [FromForm] string CourseId, [FromForm] IEnumerable<IFormFile> files)
        {
			var webRootPath = _env.WebRootPath;
			if (!Directory.Exists(webRootPath + "\\Document\\"))
			{
				Directory.CreateDirectory(webRootPath + "\\Document\\");
			}
			DocumentManagement docManage = new DocumentManagement();
            DocumentFileManagement filesManage = new DocumentFileManagement();
            var now = DateTime.Now;
            docManage.AddDocument(new Document
            {
                AccountId = AccountId,
                CourseId = CourseId,
                DocumentCreate = now,
            });
            Document savedDoc = docManage.GetDocbyDate(now, AccountId);
            if (files != null && files.Count() > 0)
            {
                foreach (var file in files)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        var fileBytes = stream.ToArray();
                        var fileName = file.FileName;

                        // TODO: Store the file or perform other operations on it as needed
                        filesManage.AddDocumentFile(new DocumentFile
                        {
                            UploadFile = file.FileName,
                            FileType = FileType.Document,
                            DocumentationId = savedDoc.DocumentId
                        });
                        // Example response to return to frontend
                        return Ok(new { message = "File uploaded successfully" });
                    }
                }
            }

            // If no files were uploaded
            return BadRequest("No files were uploaded");
        }
    }
}
