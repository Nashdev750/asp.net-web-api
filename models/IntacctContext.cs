namespace Intacct.Models;
using Microsoft.EntityFrameworkCore;

public class IntacctContext: DbContext
{
  public IntacctContext(DbContextOptions<IntacctContext> options)
        : base(options)
    {
    }

    public DbSet<User> users { get; set; } = null!;
    public DbSet<TimeSheet> TimeSheets { get; set; } = null!;
}


