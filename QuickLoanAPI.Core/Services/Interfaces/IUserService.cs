using QuickLoanAPI.Application.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Services.Interfaces
{
  public interface IUserService
  {
    Task<(string, UserType)> Login(LoginModel userLogin);
    Task<string> Registration(UserRegistration userRegistration);
  }
}
