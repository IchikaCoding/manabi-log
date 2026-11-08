namespace ManabiLog.Models
{
    public class StudyRecord
    {
        public int Id { get; set; }
        public DateOnly StudyDate { get; set; }
        public string Title { get; set; } = "";
        // タグはここに記載予定
        // アクセス修飾子と変数名の間には型を書く。この時プロパティ指定とかはNG。プロパティは型ではない！
        public List<Tag>? Tags { get; set; }
        public string? Content { get; set; }
        public string? Learned { get; set; }
        public string? Memo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
