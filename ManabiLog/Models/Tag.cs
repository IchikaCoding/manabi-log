namespace ManabiLog.Models
{
    // staticにはしない。複数のタグを持つ設定だから。
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        // 次は、CRUD実装させておいてデータで足りないことを知る
    }
}