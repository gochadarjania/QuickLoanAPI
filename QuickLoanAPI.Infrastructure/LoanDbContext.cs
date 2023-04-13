using Microsoft.EntityFrameworkCore;
using QuickLoanAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Infrastructure
{
  public class LoanDbContext : DbContext
  {
    public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<LoanRequest> LoanRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
          .HasOne(u => u.UserInfo)
          .WithOne(ui => ui.User)
          .HasForeignKey<UserInfo>(ui => ui.UserId);
    }
  }
}
