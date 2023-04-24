using Microsoft.EntityFrameworkCore;
using QuickLoanAPI.Domain;
using QuickLoanAPI.Infrastructure.DbManage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Infrastructure.DbManage
{
  public class UserRepository : IUserRepository
  {
    private readonly LoanDbContext _db;
    public UserRepository(LoanDbContext db)
    {
      _db = db; 
    }
    public async Task<string> CreateUser(User user)
    {
      await _db.Users.AddAsync(user);
      await _db.SaveChangesAsync();
      return user.Email;
    }
    public async Task<bool> CheckEmail(string email)
    {
      var result = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

      return result == null ? false : true;
    }

    public async Task<User> Login(string email)
    {
      return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
  }
}
