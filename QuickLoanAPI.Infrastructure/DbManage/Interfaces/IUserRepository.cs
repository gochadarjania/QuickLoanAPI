using QuickLoanAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Infrastructure.DbManage.Interfaces
{
  public interface IUserRepository
  {
    Task<bool> CheckEmail(string email);
    Task<User> Login(string email);
    Task<string> CreateUser(User user);
  }
}
