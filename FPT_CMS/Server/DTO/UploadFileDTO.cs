namespace Server.DTO
{
	public class UploadFileDTO
	{
		public int Id { get; set; }
        public int topicId { get; set; }
        public IFormFile files { get; set; }
		public string Name { get; set; }
	}
}
