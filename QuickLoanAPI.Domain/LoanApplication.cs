using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Domain
{
  public class LoanApplication
  {
    public int Id { get; set; }

    public int LoanRequestId { get; set; }
    public LoanRequest LoanRequest { get; set; }

    public DateTime? ApprovedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
  }
}
