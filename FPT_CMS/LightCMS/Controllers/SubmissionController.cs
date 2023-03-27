using LightCMS.DTO;
using LightCMS.Services;
using LightCMS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace LightCMS.Controllers
{
	public class SubmissionController : Controller
	{
        private readonly HttpClient client = null;
        private string CmsApiUrl = "";
        private BaseService jwtService = new BaseService();

        public SubmissionController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> ShowAll(int topicId)
		{
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            string strSubmissions = await jwtService.GetObjects("http://localhost:5195/api/Submission/GetSubmissions/" + topicId, this.client);
            IEnumerable<SubmissionDTO> submissions = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SubmissionDTO>>(strSubmissions);

            string strWebRootPath =
                await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Base/GetWebRootPath"),
                    this.client);
            ViewBag.WebRootPath = strWebRootPath;

            ViewData["topicId"] = topicId;

            return View(submissions);
        }
	}
}
