using LightCMS.DTO;
using LightCMS.Services;
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

            return View(submissions);
        }
	}
}
