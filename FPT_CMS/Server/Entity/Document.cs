using System.Text.Json.Serialization;

namespace Server.Entity
{
    public class Document
    {
        public int DocumentId { get; set; }
        public DateTime DocumentCreate { get; set; }
        [JsonIgnore] public string CourseId { get; set; }
        [JsonIgnore] public string AccountId { get; set; }
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
        public virtual Course? Course { get; set; }
        public virtual Account? Account { get; set; }
    }
}