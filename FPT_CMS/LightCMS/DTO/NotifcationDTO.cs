using System.Diagnostics.CodeAnalysis;

namespace LightCMS.DTO
{
	public class NotifcationDTO
	{
		public int NotificationId { get; set; }
		public string Text { get; set; }
		public string UploadFile { get; set; }
		public string CourseId { get; set; }
	}
}
