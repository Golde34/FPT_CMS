using Server.Entity.Enum;

namespace Server.Entity
{
	public class DocumentFile
	{
		public int Id { get; set; }
		public string UploadFile { get; set; }
		public FileType FileType { get; set; }
		public int DocumentationId { get; set; }

		public virtual Document? Document { get; set; }

	}
}
