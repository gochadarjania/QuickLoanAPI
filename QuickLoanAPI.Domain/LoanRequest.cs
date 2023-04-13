using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Domain
{
  public class LoanRequest
  {
    public int Id { get; set; }
    public int Period { get; set; }
    public decimal Money { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int LoanTypeId { get; set; }
    public LoanType LoanType { get; set; }

    public int CurrencyTypeId { get; set; }
    public CurrencyType CurrencyType { get; set; }

    public int StatusId { get; set; }
    public Status Status { get; set; }

    public ICollection<LoanApplication> LoanApplications { get; set; }
  }
}
