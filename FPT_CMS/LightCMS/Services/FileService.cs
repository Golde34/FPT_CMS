using LightCMS.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace LightCMS.Services
{
    public class FileService : Controller
    {
        public async Task<ActionResult> NotificationFileHandler(string session, IFormFile file, NotificationDTO notificationDTO)
        {
            HttpResponseMessage response;
            if (file != null && file.Length > 0)
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        // GET JWT AND END IT ALONG WITH THE REQUEST
                        if (session != null)
                        {
                            var token = session.Replace('"', ' ').Trim();
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
                            response = await client.PostAsync("http://localhost:5195/api/Notification/AddNotification",
                                content);
                            if (!response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Detail");
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
