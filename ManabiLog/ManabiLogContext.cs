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

        modelBuilder.Entity<StudyRecord>()
            .HasMany(record => record.Tags)
            .WithMany(tag => tag.StudyRecords)
            .UsingEntity<JunctionTableModel>(
                join => join
                    .HasOne(x => x.tag)
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