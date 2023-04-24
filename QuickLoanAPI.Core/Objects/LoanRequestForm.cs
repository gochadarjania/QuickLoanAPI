using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Objects
{
  public class LoanRequestForm
  {
    public int Id { get; set; }
    public int Period { get; set; }
    public decimal Money { get; set; }
    public LoanType LoanTypeId { get; set; }
    public CurrencyType CurrencyTypeId { get; set; }
    public ProgressStatus StatusId { get; set; }
    public int? userId { get; set; }
  }
}
