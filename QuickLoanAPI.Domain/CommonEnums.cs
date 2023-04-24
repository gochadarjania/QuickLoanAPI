using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application
{
  public enum UserType
  {
    Admin = 1,
    Customer
  }
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
  public enum ValidatorType
  {
    UserLogin = 1,
    UserRegistration,
    LoanRequest
  }
}
