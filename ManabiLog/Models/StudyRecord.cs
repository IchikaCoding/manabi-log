namespace ManabiLog.Models
{
    public class StudyRecord
    {
        public int Id { get; set; }
        public DateOnly StudyDate { get; set; }
        public string Title { get; set; } = "";
        // タグはここに記載予定
        public string? Content { get; set; }
        public string? Learned { get; set; }
        public string? Memo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
