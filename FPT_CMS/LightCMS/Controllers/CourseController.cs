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
        private BaseService jwtService;
        private FileService fileService;

        public CourseController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            jwtService = new BaseService();
            fileService = new FileService();
        }

        public async Task<IActionResult> Index()
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            // Get Courses
            string strCourse = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Courses/GetCourses"), this.client);
            dynamic courses = JsonConvert.DeserializeObject<IEnumerable<CourseDTO>>(strCourse);

            return View(courses);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string query)
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            // Get Courses
            string strCourse = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Courses/GetCourses"), this.client);
            List<CourseDTO> courses = (List<CourseDTO>) 
                JsonConvert.DeserializeObject<IEnumerable<CourseDTO>>(strCourse);

            return View(courses.Where(c => c.CourseName.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        public async Task<IActionResult> RegisteredCourse()
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            // Get Registered Courses
            string strCourse = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Courses/GetRegisteredCourses"), this.client);
            dynamic courses = JsonConvert.DeserializeObject<IEnumerable<CourseDTO>>(strCourse);

            return View(courses);
        }

        public async Task<IActionResult> ManagedCourse()
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            // Get Registered Courses
            string strCourse = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Courses/GetManagedCourses"), this.client);
            dynamic courses = JsonConvert.DeserializeObject<IEnumerable<CourseDTO>>(strCourse);

            return View(courses);
        }

        public async Task<IActionResult> Add()
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            //Get Subjects
            string strSubject = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Subject/GetSubjects"), this.client);
            dynamic? subjects = JsonConvert.DeserializeObject<IEnumerable<SubjectDTO>>(strSubject);
            ViewBag.Subject = subjects;

            //Get Semesters
            string strSemester =
                await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Semester/GetSemesters"),
                    this.client);
            dynamic? semester = JsonConvert.DeserializeObject<IEnumerable<SemesterDTO>>(strSemester);
            ViewBag.Semester = semester;

            //Get last course
            string strlastCourse = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Courses/GetLastCourse"), this.client);
            dynamic? lastCourse = JsonConvert.DeserializeObject<CourseDTO>(strlastCourse);
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

            // Course POST Method
            if (ModelState.IsValid)
            {
                courseDTO.TeacherId = "string";
                strData = JsonConvert.SerializeObject(courseDTO);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                response = await client.PostAsync(CustomAPIDirection.GetCustomAPIDirection("Courses/AddCourse"), content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ManagedCourse");
                }
            }

            string strSubject = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Subject/GetSubjects"), this.client);
            dynamic? subjects = JsonConvert.DeserializeObject<IEnumerable<SubjectDTO>>(strSubject);
            ViewBag.Subject = subjects;

            string strSemester = await jwtService.GetObjects(CustomAPIDirection.GetCustomAPIDirection("Semester/GetSemesters"), this.client);
            dynamic? semester = JsonConvert.DeserializeObject<IEnumerable<SemesterDTO>>(strSemester);
            ViewBag.Semester = semester;

            return View(courseDTO);
        }

        public async Task<IActionResult> Enroll(string courseId)
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            string strData = JsonConvert.SerializeObject(new
            {
                id = courseId
            });
            HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(CustomAPIDirection.GetCustomAPIDirection("Enrollments/AddEnroll"), content);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("RegisteredCourse");
            }

            return RedirectToAction("Detail", new { id = courseId });
        }

        public async Task<IActionResult> Unenroll(string courseId)
        {
            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            // Enrollment Delete Method
            HttpResponseMessage response = await client.DeleteAsync(CustomAPIDirection.GetCustomAPIDirection("Enrollments/DeleteEnroll?courseId=" + courseId));
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("RegisteredCourse");
        }

        //Course Detail
        public async Task<IActionResult> Detail(int id)
        {
            jwtService.CheckLoggedIn(HttpContext.Session.GetString("isLoggedIn"));

            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            // Get Notifications
            string strNotification = await jwtService.GetObjects(
                    CustomAPIDirection.GetCustomAPIDirection("Notification/GetNotifications/" + id), this.client);
            IEnumerable<NotificationDTO> notifications = JsonConvert.DeserializeObject<IEnumerable<NotificationDTO>>(strNotification);
            ViewBag.Notification = notifications;

            // Get Comments
            Dictionary<object, dynamic> commentsDict = new Dictionary<object, dynamic>();
            foreach (var noti in notifications)
            {
                string strComment = await jwtService.GetObjects(
                    CustomAPIDirection.GetCustomAPIDirection("Notification/GetComments/" + noti.NotificationId), this.client);
                dynamic? comments = JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(strComment);
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
            string strData;

            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);
            await fileService.NotificationFileHandler(HttpContext.Session.GetString("JWT"), file, notificationDTO);

            //Get Notifications
            string strNotification = await jwtService.GetObjects(
                CustomAPIDirection.GetCustomAPIDirection("Notification/GetNotifications/" + notificationDTO.CourseId), this.client);
            IEnumerable<NotificationDTO> notifications = JsonConvert.DeserializeObject<IEnumerable<NotificationDTO>>(strNotification);
            ViewBag.Notification = notifications;

            // Get Comments
            Dictionary<object, dynamic> commentsDict = new Dictionary<object, dynamic>();
            foreach (var noti in notifications)
            {
                string strComment = await jwtService.GetObjects(
                    CustomAPIDirection.GetCustomAPIDirection("Notification/GetComments/" + noti.NotificationId), this.client);
                dynamic? comments = JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(strComment);
                commentsDict.Add(noti.NotificationId, comments);
            }
            ViewBag.Comment = commentsDict;

            ViewBag.CourseId = notificationDTO.CourseId;

            return View("Detail");
        }

        public async Task<IActionResult> AddComment([FromForm] string text, [FromForm] string accountId,
            [FromForm] int notificationId, [FromForm] string usernameComment, [FromForm] string courseId)
        {
            HttpResponseMessage response;
            string strData;

            jwtService.JWTToken(HttpContext.Session.GetString("JWT"), this.client);

            CommentDTO commentDTO = new CommentDTO
            {
                AccountId = accountId,
                NotificationID = notificationId,
                Text = text,
                Username = usernameComment
            };
            if (ModelState.IsValid)
            {
                strData = JsonConvert.SerializeObject(commentDTO);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                response = await client.PostAsync(CustomAPIDirection.GetCustomAPIDirection("Notification/AddComment"), content);
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            // Get Notifications
            string strNotification = await jwtService.GetObjects(
                CustomAPIDirection.GetCustomAPIDirection("Notification/GetNotifications/" + courseId), this.client);
            IEnumerable<NotificationDTO> notifications =
                Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<NotificationDTO>>(strNotification);
            ViewBag.Notification = notifications;

            // Get Comments
            Dictionary<object, dynamic> commentsDict = new Dictionary<object, dynamic>();
            foreach (var noti in notifications)
            {
                string strComment = await jwtService.GetObjects(
                    CustomAPIDirection.GetCustomAPIDirection("Notification/GetComments/" + noti.NotificationId),
                    this.client);
                dynamic? comments = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<CommentDTO>>(strComment);
                commentsDict.Add(noti.NotificationId, comments);
            }

            ViewBag.Comment = commentsDict;

            ViewBag.CourseId = notifications.First().CourseId;

            return View("Detail");
        }
    }
}