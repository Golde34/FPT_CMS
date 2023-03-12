using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using LightCMS.DTO;
using LightCMS.Utils;
using Newtonsoft.Json;

namespace LightCMS.Controllers
{
    public class CourseController : Controller
    {
        private readonly HttpClient client = null;
        private string CmsApiUrl = "";

        public CourseController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CmsApiUrl = "http://localhost:5195/api/Courses";
        }

        public async Task<IActionResult> Index()
        {
            // GET JWT AND END IT ALONG WITH THE REQUEST
            if (HttpContext.Session.GetString("JWT") != null)
            {
                var token = HttpContext.Session.GetString("JWT").Replace('"', ' ').Trim();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            }

            HttpResponseMessage response = await client.GetAsync(CmsApiUrl + "/GetCourses");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            string strData = await response.Content.ReadAsStringAsync();
            dynamic courses = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CourseDTO>>(strData);
            return View(courses);
        }

        public async Task<IActionResult> Add()
        {
            if (HttpContext.Session.GetString("JWT") != null)
            {
                var token = HttpContext.Session.GetString("JWT").Replace('"', ' ').Trim();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            }

            HttpResponseMessage response =
                await client.GetAsync(CustomAPIDirection.GetCustomAPIDirection("Subject/GetSubjects"));

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            string strData = await response.Content.ReadAsStringAsync();
            dynamic? subjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SubjectDTO>>(strData);

            ViewBag.Subject = subjects;

            response = await client.GetAsync(CustomAPIDirection.GetCustomAPIDirection("Semester/GetSemesters"));
            strData = await response.Content.ReadAsStringAsync();
            dynamic? semester = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SemesterDTO>>(strData);
            ViewBag.Semester = semester;

            response = await client.GetAsync(CmsApiUrl + "/GetLastCourse");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            strData = await response.Content.ReadAsStringAsync();
            dynamic? lastCourse = Newtonsoft.Json.JsonConvert.DeserializeObject<CourseDTO>(strData);
            string courseId = Convert.ToString(Convert.ToInt32(lastCourse.CourseId) + 1);
            CourseDTO course = new CourseDTO
            {
                CourseId = courseId
            };
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CourseDTO courseDTO)
        {
            HttpResponseMessage response;
            string strData;
            if (HttpContext.Session.GetString("JWT") != null)
            {
                var token = HttpContext.Session.GetString("JWT").Replace('"', ' ').Trim();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            }


            if (ModelState.IsValid)
            {
                strData = Newtonsoft.Json.JsonConvert.SerializeObject(courseDTO);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                response = await client.PostAsync(CmsApiUrl + "/AddCourse", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            response =
                await client.GetAsync(CustomAPIDirection.GetCustomAPIDirection("Subject/GetSubjects"));

            strData = await response.Content.ReadAsStringAsync();
            dynamic? subjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SubjectDTO>>(strData);

            ViewBag.Subject = subjects;

            response = await client.GetAsync(CustomAPIDirection.GetCustomAPIDirection("Semester/GetSemesters"));
            strData = await response.Content.ReadAsStringAsync();
            dynamic? semester = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SemesterDTO>>(strData);
            ViewBag.Semester = semester;

            return View(courseDTO);
        }

        public async Task<IActionResult> Detail(string courseId)
        {
            // GET JWT AND END IT ALONG WITH THE REQUEST
            if (HttpContext.Session.GetString("JWT") != null)
            {
                var token = HttpContext.Session.GetString("JWT").Replace('"', ' ').Trim();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            }

            HttpResponseMessage response = await client.GetAsync(CmsApiUrl + "/GetCourseByID/" + courseId);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            string strData = await response.Content.ReadAsStringAsync();
            dynamic course = Newtonsoft.Json.JsonConvert.DeserializeObject<CourseDTO>(strData);
            return View(course);
        }

        public async Task<IActionResult> Topic(string courseId)
        {
            // GET JWT AND END IT ALONG WITH THE REQUEST
            if (HttpContext.Session.GetString("JWT") != null)
            {
                var token = HttpContext.Session.GetString("JWT").Replace('"', ' ').Trim();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            }

            HttpResponseMessage response = await client.GetAsync("http://localhost:5195/api/Topic/GetTopicsByCourseId/"+courseId);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            string strData = await response.Content.ReadAsStringAsync();
            IEnumerable<TopicDTO> topics = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TopicDTO>>(strData);

            // Send courseId to View
            ViewData["courseId"] = courseId;

            return View(topics.OrderByDescending(t => t.Id));
        }

        public async Task<IActionResult> TopicContent(int topicId)
        {
            // GET JWT AND END IT ALONG WITH THE REQUEST
            if (HttpContext.Session.GetString("JWT") != null)
            {
                var token = HttpContext.Session.GetString("JWT").Replace('"', ' ').Trim();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            }

            HttpResponseMessage response = await client.GetAsync("http://localhost:5195/api/Topic/GetTopicById/" + topicId);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            string strData = await response.Content.ReadAsStringAsync();
            dynamic topic = Newtonsoft.Json.JsonConvert.DeserializeObject<TopicDTO>(strData);
            
            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> TopicContent(UploadFileDTO fileDTO)
        {
            if(fileDTO.files.Length > 0)
            {
                try
                {
                    string strData = JsonConvert.SerializeObject(fileDTO);
                    HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(CmsApiUrl, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction("TopicContent", new { topicId = fileDTO.topicId });
        }
    }
}