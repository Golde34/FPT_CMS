﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LightCMS.DTO
{
	public class NotificationDTO
	{
		public int NotificationId { get; set; }
		[Required]
		public string Text { get; set; }
		public string UploadFile { get; set; }
		[Required]
		public string CourseId { get; set; }
		[Required]
		public string AccountId { get; set; }
		public string Username { get; set; }
	}
}
