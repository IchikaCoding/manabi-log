using ManabiLog.Models;
using Microsoft.EntityFrameworkCore;

public class ManabiLogContext(
    DbContextOptions<ManabiLogContext> options
) : DbContext(options)
{
    public DbSet<StudyRecord> StudyRecord { get; set; } = default!;

    // TODO: `!`これはなに？
    public DbSet<JunctionTableModel> StudyRecordTags { get; set; }
        = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // HasManyとは？EF Coreの一対多の多側を定義する設定用メソッド
        modelBuilder.Entity<StudyRecord>()
            .HasMany(record => record.Tags)
            // Tag（多）は1つのStudyRecords（一）に属する
            .WithMany(tag => tag.StudyRecords)
            // 使用するモデルを指定する
            .UsingEntity<JunctionTableModel>(
                join => join
                // 中間テーブルの1行は、`TagsId` を使って1つのタグにつながる
                    .HasOne(x => x.tag)
                    // ? タグ側に、中間モデルの一覧を持つプロパティは指定しないらしい
                    .WithMany()
                    .HasForeignKey(x => x.TagsId),

                join => join
                    .HasOne(x => x.studyRecord)
                    .WithMany()
                    .HasForeignKey(x => x.StudyRecordsId),

                join =>
                {
                    join.ToTable("StudyRecordTag");

                    join.HasKey(x => new
                    {
                        x.StudyRecordsId,
                        x.TagsId
                    });
                });
    }
}