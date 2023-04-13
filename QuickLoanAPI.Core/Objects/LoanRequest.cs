using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Core.Objects
{
  public class LoanRequest
  {
    public int Period { get; set; }
    public decimal Money { get; set; }
    public int UserId { get; set; }
    public LoanType LoanTypeId { get; set; }
    public CurrencyType CurrencyTypeId { get; set; }
    public ProgressStatus StatusId { get; set; }
  }
}
