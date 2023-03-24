using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using LightCMS.DTO;
using LightCMS.Utils;
using LightCMS.Services;
using Newtonsoft.Json;

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
			IEnumerable<NotificationDTO> notifications = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<NotificationDTO>>(strNotification);
            ViewBag.Notification = notifications;

            foreach (var noti in notifications)
            {
                string strComment = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Notification/GetComments/" + noti.NotificationId), this.client);
                dynamic? comments = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(strComment);
                commentsDict.Add(noti.NotificationId, comments); 
            }
            ViewBag.Comment = commentsDict;

            string strWebRootPath = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Base/GetWebRootPath"), this.client);
            ViewBag.WebRootPath = strWebRootPath;

            ViewBag.CourseId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification(NotificationDTO notificationDTO, [FromForm] IFormFile file)
        {
            HttpResponseMessage response;
            string strData;
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

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
                        content.Add(new StringContent(notificationDTO.AccountId), "AccountId");
                        content.Add(new StringContent(notificationDTO.CourseId), "CourseId");
                        content.Add(new StringContent(notificationDTO.Text.ToString()), "Text");

                        if (ModelState.IsValid)
                        {
                            response = await client.PostAsync("http://localhost:5195/api/Notification/AddNotification", content);
                            if (!response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Detail");
                            }
                        }
                    }
                }
            }
            
            Dictionary<object, dynamic> commentsDict = new Dictionary<object, dynamic>();
            //Get Notifications
            string strNotification = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Notification/GetNotifications/" + notificationDTO.CourseId), this.client);
            IEnumerable<NotificationDTO> notifications = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<NotificationDTO>>(strNotification);
            ViewBag.Notification = notifications;

            foreach (var noti in notifications)
            {
                string strComment = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Notification/GetComments/" + noti.NotificationId), this.client);
                dynamic? comments = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(strComment);
                commentsDict.Add(noti.NotificationId, comments);
            }
            ViewBag.Comment = commentsDict;

            ViewBag.CourseId = notificationDTO.CourseId;

            return View("Detail");
        }

        public async Task<IActionResult> AddComment([FromForm] string text, [FromForm] string accountId, [FromForm] int notificationId, [FromForm] string courseId)
        {
            HttpResponseMessage response;
            string strData;
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);
            CommentDTO commentDTO = new CommentDTO
            {
                AccountId = accountId,
                NotificationID = notificationId,
                Text = text
            };
            if (ModelState.IsValid)
            {
                strData = Newtonsoft.Json.JsonConvert.SerializeObject(commentDTO);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                response = await client.PostAsync("http://localhost:5195/api/Notification/AddComment", content);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            Dictionary<object, dynamic> commentsDict = new Dictionary<object, dynamic>();
            //Get Notifications
            string strNotification = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Notification/GetNotifications/" + courseId), this.client);
            IEnumerable<NotificationDTO> notifications = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<NotificationDTO>>(strNotification);
            ViewBag.Notification = notifications;

            foreach (var noti in notifications)
            {
                string strComment = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Notification/GetComments/" + noti.NotificationId), this.client);
                dynamic? comments = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(strComment);
                commentsDict.Add(noti.NotificationId, comments);
            }
            ViewBag.Comment = commentsDict;

            ViewBag.CourseId = notifications.First().CourseId;

            return View("Detail");
        }
    }
}