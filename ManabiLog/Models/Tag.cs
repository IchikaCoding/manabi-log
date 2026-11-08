namespace ManabiLog.Models
{
    // staticにはしない。複数のタグを持つ設定だから。
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        // どうして空のListを作成するの？
        // 👉️null許容で作成すると、List自体がnullの可能性がある。nullの場合、Listの中身見るforeachの処理とかでNullReferenceExceptionのエラーでプログラムが落ちる。
        // 最初から空のListを作成しておくと、中身が0件なだけでforeachでもエラーが出ない✨️
        // どうしてここでStudyRecordを参照するのか？
        // 👉️お互いが参照することで多対多の関係になる。EF Coreが自動で中間テーブルを作成してくれるらしい。
        public List<StudyRecord> studyRecords { get; set; } = new();
    }
}