namespace Server.Entity
{
	public class Document
	{
		public Document()
		{
			Course = new Course();
			Account= new Account();
		}
		public int DocumentId { get; set; }
		public DateTime DocumentCreate { get; set; }
		public string CourseId { get; set; }
		public string AccountId { get; set; }
		public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
		public virtual Course? Course { get; set; }
		public virtual Account? Account { get; set; }
	}
}
