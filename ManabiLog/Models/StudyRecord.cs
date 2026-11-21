using System.ComponentModel.DataAnnotations;

namespace ManabiLog.Models
{
    public class StudyRecord
    {
        public int Id { get; set; }
        public DateOnly StudyDate { get; set; }
        [Required(ErrorMessage = "タイトルは必須項目です！")]
        // エラーがでたときにModelStateに記録したいメッセージ（？）
        [StringLength(50, ErrorMessage = "{0}は{1}文字以内で入力してください。文字数が多すぎます。")]
        public string Title { get; set; } = "";
        // タグはここに記載予定
        // アクセス修飾子と変数名の間には型を書く。この時プロパティ指定とかはNG。プロパティは型ではない！
        [Required]
        public required List<Tag> Tags { get; set; } = new();
        public string? Content { get; set; }
        public string? Learned { get; set; }
        public string? Memo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
