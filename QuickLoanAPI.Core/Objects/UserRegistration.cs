using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Objects
{
  public class UserRegistration
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }
}
