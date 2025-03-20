using Microsoft.EntityFrameworkCore;

namespace ExcelTable.DataAccess;

public class ExcelTableDbContext(DbContextOptions<ExcelTableDbContext> options) : DbContext(options)
{
    public DbSet<Meeting> Meetings { get; set; }
}