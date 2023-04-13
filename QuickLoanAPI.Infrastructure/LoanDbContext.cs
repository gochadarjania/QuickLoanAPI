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
    public DbSet<CurrencyType> CurrencyTypes { get; set; }
    public DbSet<LoanType> LoanTypes { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<LoanRequest> LoanRequests { get; set; }
    public DbSet<LoanApplication> LoanApplications { get; set; }
  }
}
