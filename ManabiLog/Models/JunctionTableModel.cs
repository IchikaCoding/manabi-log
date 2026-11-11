namespace ManabiLog.Models
{
    public class JunctionTableModel
    {
        public int StudyRecordsId { get; set; }
        public StudyRecord studyRecord { get; set; } = null!;
        public int TagsId { get; set; }
        public Tag tag { get; set; } = null!;
        // タグをつけた時間を記録するプロパティ
        public DateTime AddedTagAt { get; set; }
    }
}