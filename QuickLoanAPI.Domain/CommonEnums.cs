using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Core
{
  public enum CurrencyType
  {
    GEL = 1,
    USD,
    EUR
  }
  public enum LoanType
  {
    Quick = 1,
    Car,
    Installment
  }
  public enum ProgressStatus
  {
    Forwarded = 1,
    InProcess,
    Approved,
    Rejected
  }
}
