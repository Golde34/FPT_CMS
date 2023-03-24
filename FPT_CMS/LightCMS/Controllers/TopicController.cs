using LightCMS.DTO;
using LightCMS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace LightCMS.Controllers
{
	public class TopicController : Controller
	{
        private BaseService jwtService = new BaseService();
        private readonly HttpClient client = null;

        public TopicController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        //Course Topic
        public async Task<IActionResult> Index(string courseId)
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            string strTopic = await jwtService.GetObjects("http://localhost:5195/api/Topic/GetTopicsByCourseId/" + courseId, this.client);
            IEnumerable<TopicDTO> topics = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TopicDTO>>(strTopic);

            // Send courseId to View
            ViewData["courseId"] = courseId;

            return View(topics.OrderByDescending(t => t.Id));
        }

        public async Task<IActionResult> Content(int topicId)
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            string strTopic = await jwtService.GetObjects("http://localhost:5195/api/Topic/GetTopicById/" + topicId, this.client);
            dynamic topic = Newtonsoft.Json.JsonConvert.DeserializeObject<TopicDTO>(strTopic);

            if (HttpContext.Session.GetString("Role").Equals("Student"))
            {
                string strSubmission = await jwtService.GetObjects("http://localhost:5195/api/Submission/GetSubmission/" + topicId, this.client);
                if (strSubmission != null)
                {
                    dynamic submission = Newtonsoft.Json.JsonConvert.DeserializeObject<SubmissionDTO>(strSubmission);
                    ViewData["submission"] = submission;
                }
            }

            if (HttpContext.Session.GetString("Role").Equals("Teacher"))
            {
                string strSubmissions = await jwtService.GetObjects("http://localhost:5195/api/Submission/GetSubmissions/" + topicId, this.client);
                if (strSubmissions != null)
                {
                    dynamic submissions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SubmissionDTO>>(strSubmissions);
                    ViewData["submissions"] = submissions;
                }
            }

            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Content(IFormFile file, int topicId, string name)
        {
            if (file != null && file.Length > 0)
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

                        // Add file content
                        var fileContent = new StreamContent(file.OpenReadStream());
                        content.Add(fileContent, "file", file.FileName);

                        // Add other parameters
                        content.Add(new StringContent(name), "name");
                        content.Add(new StringContent(topicId.ToString()), "topicId");

                        // Send the request to the API
                        var response = await client.PostAsync("http://localhost:5195/api/Submission/AddSubmission", content);

                        // Process the response as needed
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("TopicContent", new { topicId = topicId });
                        }
                    }
                }
            }
            return RedirectToAction("TopicContent", new { topicId = topicId });
        }
    }
}
