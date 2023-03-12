using LightCMS.DTO;
using LightCMS.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace LightCMS.Services
{
	public class BaseService : Controller
	{
		public void JWTToken(string session, HttpClient client)
		{
			// GET JWT AND END IT ALONG WITH THE REQUEST
			if (session != null)
			{
				var token = session.Replace('"', ' ').Trim();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
			}
		}

		public async Task<string> GetObjects(string _object, HttpClient _client)
		{
			HttpResponseMessage _response = await _client.GetAsync(_object);
			CheckObject(_response);
			string strData = await _response.Content.ReadAsStringAsync();
			return strData;
		}

		public async Task<ActionResult> CheckObject(HttpResponseMessage _response)
		{
			if (!_response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Home");
			}
			return null;
		}
	}
}
