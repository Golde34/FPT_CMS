﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using LightCMS.DTO;

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
            CmsApiUrl = "localhost:5195/api/Courses";
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

            // HttpResponseMessage response = await client.GetAsync(CmsApiUrl + "/GetCourses");
            HttpResponseMessage response =
                await client.GetAsync(CmsApiUrl.Split("/")[0] + CmsApiUrl.Split("/")[1] + "/Semester/GetSemester");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            string strData = await response.Content.ReadAsStringAsync();
            dynamic? semesters = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SemesterDTO>>(strData);
            
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Topic()
        {
            return View();
        }

        public IActionResult TopicContent()
        {
            return View();
        }
    }
}