using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public BaseController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult GetWebRootPath()
        {
            string webRootPath = _env.WebRootPath;
            return Ok(webRootPath);
        }

        public async Task<IActionResult> DownloadNotificationFile(string filename)
        {
            var filepath = _env.WebRootPath + "\\Notification\\" + filename;

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }

        public async Task<IActionResult> DownloadSubmissionFile(string filename)
        {
			var filepath = _env.WebRootPath + "\\Submission\\" + filename;

			var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }

        public async Task<IActionResult> DownloadDocumentFile(string filename)
        {
            var filepath = _env.WebRootPath + "\\Document\\" + filename;

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }
    }
}
