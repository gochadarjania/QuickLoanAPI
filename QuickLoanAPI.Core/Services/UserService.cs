using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuickLoanAPI.Application.Helpers;
using QuickLoanAPI.Application.Objects;
using QuickLoanAPI.Application.Services.Interfaces;
using QuickLoanAPI.Application.Validators;
using QuickLoanAPI.Domain;
using QuickLoanAPI.Domain.Helpers;
using QuickLoanAPI.Infrastructure.DbManage.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Application.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepositpry;
    private readonly AppSettings _appSettings;
    public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
      _userRepositpry = userRepository;
    }
    public async Task<(string, UserType)> Login(LoginModel userLogin)
    {
      var emailExist = await _userRepositpry.CheckEmail(userLogin.Email);

      if (!emailExist)
      {
        throw new ClientException($"{userLogin.Email} There is no user with such email");
      }

      var user = await _userRepositpry.Login(userLogin.Email);

      if (!CheckPassword(userLogin.Password, user.Password))
      {
        throw new ClientException("Email or password is incorrect.");
      }

      var token = GenerateToken(user);

      return (token, user.UserType);
    }
    public async Task<string> Registration(UserRegistration userRegistration)
    {
      var userValidator = new UserRegistrationValidator();
      var result = userValidator.Validate(userRegistration);
      if (!result.IsValid)
      {
        throw new ClientException(string.Join("\n", result.Errors));
      }
      var emailExist = await _userRepositpry.CheckEmail(userRegistration.Email);
      if (emailExist)
      {
        throw new ClientException("Sorry, the user is already registered with this email. Please try logging in or use a different email address to register.");
      }

      var user = userRegistration.MapUser();

      return await _userRepositpry.CreateUser(user);
    }
    private bool CheckPassword(string password, string passwordHash)
    {
      byte[] hashBytes = Convert.FromBase64String(passwordHash);
      byte[] salt = new byte[16];
      Array.Copy(hashBytes, 0, salt, 0, 16);

      using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
      {
        byte[] hash = pbkdf2.GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
          if (hashBytes[i + 16] != hash[i])
          {
            return false;
          }
        }
        return true;
      }
    }
    private string GenerateToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.UserType.ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

        return tokenString;      
    }
  }
}
