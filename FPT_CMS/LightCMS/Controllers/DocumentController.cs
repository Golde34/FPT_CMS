using LightCMS.DTO;
using LightCMS.Services;
using LightCMS.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LightCMS.Controllers
{
    public class DocumentController : Controller
    {
        private BaseService jwtService = new BaseService();
        private readonly HttpClient client = null;

        public DocumentController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index(string courseId)
        {
            if (HttpContext.Session.GetString("isLoggedIn") == null || !HttpContext.Session.GetString("isLoggedIn").Equals("true"))
            {
                return RedirectToAction("Login", "Account");
            }

            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            string strDocument = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Document/GetDocumentsByCourseId/" + courseId), this.client);
            IEnumerable<DocumentDTO> documents = JsonConvert.DeserializeObject<IEnumerable<DocumentDTO>>(strDocument);
            ViewBag.Document = documents;

            Dictionary<object, dynamic> filesDict = new Dictionary<object, dynamic>();

            if (documents != null)
            {
                foreach (var doc in documents)
                {
                    string strFiles = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Document/GetDocumentFilesByDocumentId/" + doc.DocumentId), this.client);
                    dynamic? files = JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(strFiles);
                    filesDict.Add(doc.DocumentId, files);
                }
                ViewBag.File = filesDict;
            } else
            {
                ViewBag.File = null;
            }
            

            string strWebRootPath = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Base/GetWebRootPath"), this.client);
            ViewBag.WebRootPath = strWebRootPath;

            ViewBag.CourseId = courseId;

            return View();
        }

        [HttpGet]
        public IActionResult AddDoc(string courseId)
        {
            if (HttpContext.Session.GetString("isLoggedIn") == null || !HttpContext.Session.GetString("isLoggedIn").Equals("true"))
            {
                return RedirectToAction("Login", "Account");
            }
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);
            ViewBag.CourseId = courseId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDoc([FromForm] string accountId, [FromForm] string courseId, [FromForm] List<IFormFile> files)
        {
            HttpResponseMessage response;
            string strData;
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            if (files != null && files.Count > 0)
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        // GET JWT AND END IT ALONG WITH THE REQUEST
                        if (HttpContext.Session.GetString("JWT") != null)
                        {
                            var token = HttpContext.Session.GetString("JWT").Replace('"', ' ').Trim();
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
                        }

                        // Add files content
                        for (int i = 0; i < files.Count; i++)
                        {
                            var file = files[i];
                            var fileContent = new StreamContent(file.OpenReadStream());
                            content.Add(fileContent, "files", file.FileName);
                        }
                            
                        // Add other parameters
                        content.Add(new StringContent(accountId), "AccountId");
                        content.Add(new StringContent(courseId), "CourseId");

                        if (ModelState.IsValid)
                        {
                            response = await client.PostAsync("http://localhost:5195/api/Document/AddDocument", content);
                            if (!response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
            }

            return View("Index");
        }
    }
}
