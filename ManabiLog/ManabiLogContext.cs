using ManabiLog.Models;
using Microsoft.EntityFrameworkCore;

public class ManabiLogContext(
    DbContextOptions<ManabiLogContext> options
) : DbContext(options)
{
    public DbSet<StudyRecord> StudyRecord { get; set; } = default!;
}