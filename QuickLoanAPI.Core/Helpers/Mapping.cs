using Microsoft.AspNetCore.Http;
using QuickLoanAPI.Application.Objects;
using QuickLoanAPI.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Helpers
{
  public static class Mapping
  {
    public static User MapUser(this UserRegistration model)
    {
      var user = new User();
      user.Email = model.Email;
      user.Password = GeneratePasswordHash(model.Password);
      user.UserType = UserType.Customer;

      user.UserInfo = new UserInfo()
      {
        FirstName = model.FirstName,
        LastName = model.LastName,
        PersonalNumber = model.PersonalNumber,
        DateOfBirth = (DateTime)model.DateOfBirth,
        CreatedAt = DateTime.Now
      };
      return user;
    }

    public static LoanRequest MapLoan(this LoanRequestForm model, HttpRequest httpRequest)
    {
      var loan = new LoanRequest();
      loan.LoanTypeId = model.LoanTypeId;
      loan.StatusId = ProgressStatus.Forwarded;
      loan.Period = model.Period;
      loan.Money = model.Money;
      loan.CurrencyTypeId = model.CurrencyTypeId;
      loan.RequestedAt = DateTime.Now;
      loan.UserId = httpRequest.GetUserIdbyJWT();
      return loan;
    }
    public static LoanRequest MapLoanUpdate(this LoanRequestForm model, HttpRequest httpRequest)
    {
      var loan = new LoanRequest();
      loan.LoanTypeId = model.LoanTypeId;
      loan.Period = model.Period;
      loan.Money = model.Money;
      loan.CurrencyTypeId = model.CurrencyTypeId;
      loan.UpdatedAt = DateTime.Now;
      loan.Id = model.Id;
      loan.UserId = httpRequest.GetUserIdbyJWT();
      return loan;
    }
    public static LoanRequest MapLoanUpdateByAdmin(this LoanRequestForm model)
    {
      var loan = new LoanRequest();
      loan.LoanTypeId = model.LoanTypeId;
      loan.Period = model.Period;
      loan.Money = model.Money;
      loan.CurrencyTypeId = model.CurrencyTypeId;
      loan.UpdatedAt = DateTime.Now;
      loan.Id = model.Id;
      loan.StatusId = model.StatusId;
      loan.UserId = (int)model.userId;
      return loan;
    }
    public static LoanRequestForm MapLoanToLoanForm(this LoanRequest model)
    {
      var loan = new LoanRequestForm();
      loan.Id = model.Id;
      loan.LoanTypeId = model.LoanTypeId;
      loan.StatusId = ProgressStatus.Forwarded;
      loan.Period = model.Period;
      loan.Money = model.Money;
      loan.CurrencyTypeId = model.CurrencyTypeId;
      loan.StatusId = model.StatusId;
      loan.userId = model.UserId;
      return loan;
    }public static List<LoanRequestForm> MapLoanToLoanForm(this List<LoanRequest>  model)
    {
      var loans = new List<LoanRequestForm>();
      foreach (var item in model)
      {
        var loan = new LoanRequestForm();
        loan.Id = item.Id;
        loan.LoanTypeId = item.LoanTypeId;
        loan.StatusId = item.StatusId;
        loan.Period = item.Period;
        loan.Money = item.Money;
        loan.CurrencyTypeId = item.CurrencyTypeId;
        loan.StatusId = item.StatusId;
        loans.Add(loan);
      }
      return loans;
    }    
    private static string GeneratePasswordHash(string password)
    {
      byte[] salt = new byte[16];
      using (var rng = new RNGCryptoServiceProvider())
      {
        rng.GetBytes(salt);
      }

      using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
      {
        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        return Convert.ToBase64String(hashBytes);
      }
    }
  }
}
