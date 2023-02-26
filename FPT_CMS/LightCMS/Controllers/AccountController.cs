﻿using LightCMS.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace LightCMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient client = null;
        private string CmsApiUrl = "";

        public AccountController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CmsApiUrl = "http://localhost:5195/api/token";
        }

        // Đưa tới trang đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn != null && isLoggedIn.Equals("true"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountDTO accountDTO)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonConvert.SerializeObject(accountDTO);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(CmsApiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    // Get the response
                    var token = await response.Content.ReadAsStringAsync();

                    HttpContext.Session.SetString("JWT", token);
                    HttpContext.Session.SetString("isLoggedIn", "true");
                    return RedirectToAction("Index", "Home");
                }
                                            
                ModelState.AddModelError(string.Empty, "Wrong username or password");
            }

            return View(accountDTO);
        }

        // Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT");
            HttpContext.Session.Remove("isLoggedIn");
            return RedirectToAction("Login", "Account");
        }
    }
}
