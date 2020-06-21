using Microsoft.EntityFrameworkCore;
using NorusContract.Domain.Entities;

namespace NorusContract.Date
{
  public class DataContext: DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<Contract> Contracts { get; set; }
    public DbSet<User> Users { get; set; }
  }
}