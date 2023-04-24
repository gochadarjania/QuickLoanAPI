using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickLoanAPI.Application;
using QuickLoanAPI.Application.Objects;
using QuickLoanAPI.Application.Services;
using QuickLoanAPI.Application.Services.Interfaces;
using System.Data;

namespace QuickLoanAPI.Controllers
{
  [Authorize(Roles = "Admin")]
  public class AdminController : BaseController
  {
    private IAdminService _adminService;
    public AdminController(IAdminService adminService)
    {
      _adminService = adminService;
    }

    [HttpPost("{loanId}")]
    public async Task<ActionResult<string>> Approve(int loanId)
    {
      await _adminService.Approve(loanId);
      return Ok();
    }

    [HttpDelete("{loanId}")]
    public async Task<ActionResult<string>> LoanDelete(int loanId)
    {
      await _adminService.Delete(loanId);
      return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<LoanRequestForm>>> GetLoans(int page = 1, int limit = 25)
    {
      var result = await _adminService.GetLoans(page, limit);
      return Ok(new { data = result.Item1, total = result.Item2 });
    }

    [HttpGet("{loanId}")]
    public async Task<ActionResult<LoanRequestForm>> GetLoan(int loanId)
    {
      return Ok(await _adminService.GetLoan(loanId));
    }

    [HttpPost]
    public async Task<ActionResult<string>> LoanUpdate(LoanRequestForm loan)
    {
      await _adminService.Update(loan);
      return Ok();
    }


  }
}
