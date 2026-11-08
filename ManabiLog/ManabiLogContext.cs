using Microsoft.EntityFrameworkCore;

public class ManabiLogContext(DbContextOptions<ManabiLogContext> options) : DbContext(options)
{
    public DbSet<ManabiLog.Models.StudyRecord> StudyRecord { get; set; } = default!;
}
