using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using LightCMS.DTO;
using LightCMS.Utils;
using Newtonsoft.Json;
using LightCMS.Services;
using Newtonsoft.Json.Linq;

namespace LightCMS.Controllers
{
    public class CourseController : Controller
    {
        private readonly HttpClient client = null;
        private string CmsApiUrl = "";
        private BaseService jwtService = new BaseService();

        public CourseController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CmsApiUrl = "http://localhost:5195/api/Courses";
        }

        //Course Index
        public async Task<IActionResult> Index()
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

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
			jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

			//Get Subjects
			string strSubject = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Subject/GetSubjects"), this.client);
            dynamic? subjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SubjectDTO>>(strSubject);
            ViewBag.Subject = subjects;

			//Get Semesters
			string strSemester = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Semester/GetSemesters"), this.client);
            dynamic? semester = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SemesterDTO>>(strSemester);
            ViewBag.Semester = semester;

			//Get last course
			string strlastCourse = await jwtService.GetObjects(CmsApiUrl + "/GetLastCourse", this.client);
            dynamic? lastCourse = Newtonsoft.Json.JsonConvert.DeserializeObject<CourseDTO>(strlastCourse);
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
			jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

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

			string strSubject = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Subject/GetSubjects"), this.client);
			dynamic? subjects = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SubjectDTO>>(strSubject);
            ViewBag.Subject = subjects;

			string strSemester = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Semester/GetSemesters"), this.client);
            dynamic? semester = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SemesterDTO>>(strSemester);
            ViewBag.Semester = semester;

            return View(courseDTO);
        }

        //Course Detail
		public async Task<IActionResult> Detail(int id)
        {
			jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            Dictionary<object, dynamic> commentsDict = new Dictionary<object, dynamic>();
			//Get Notifications
			string strNotification = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Notification/GetNotifications/" + id), this.client);
			dynamic? notifications = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<NotificationDTO>>(strNotification);
			ViewBag.Notification = notifications;

            foreach (var noti in notifications)
            {
                string strComment = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Notification/GetComments/" + noti.NotificationId), this.client);
                dynamic? comments = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(strComment);
                commentsDict.Add(noti.NotificationId, comments); 
            }
            ViewBag.Comment = commentsDict;

            ViewBag.CourseId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification(NotificationDTO notificationDTO)
        {
            HttpResponseMessage response;
            string strData;
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            if (ModelState.IsValid)
            {
                strData = Newtonsoft.Json.JsonConvert.SerializeObject(notificationDTO);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                response = await client.PostAsync(CustomAPIDirection.GetCustomAPIDirection("Notification/AddNotification"), content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Detail");
        }

        //Course Topic
        public async Task<IActionResult> Topic(string courseId)
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            string strTopic = await jwtService.GetObjects("http://localhost:5195/api/Topic/GetTopicsByCourseId/" + courseId, this.client);
            IEnumerable<TopicDTO> topics = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TopicDTO>>(strTopic);

            // Send courseId to View
            ViewData["courseId"] = courseId;

            return View(topics.OrderByDescending(t => t.Id));
        }

        public async Task<IActionResult> TopicContent(int topicId)
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            string strTopic = await jwtService.GetObjects("http://localhost:5195/api/Topic/GetTopicsByCourseId/" + topicId, this.client);
            dynamic topic = Newtonsoft.Json.JsonConvert.DeserializeObject<TopicDTO>(strTopic);
            
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