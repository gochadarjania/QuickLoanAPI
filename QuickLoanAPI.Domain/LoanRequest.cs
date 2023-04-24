using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickLoanAPI.Application;

namespace QuickLoanAPI.Domain
{
  public class LoanRequest
  {
    public int Id { get; set; }
    public int Period { get; set; }
    public decimal Money { get; set; }
    public LoanType LoanTypeId { get; set; }
    public CurrencyType CurrencyTypeId { get; set; }
    public ProgressStatus StatusId { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
  }
}
