using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickLoanAPI.Application.Objects;
using QuickLoanAPI.Application.Services.Interfaces;
using System.Data;

namespace QuickLoanAPI.Controllers
{
  [Authorize(Roles = "Customer")]
  public class CustomerController : BaseController
  {
    private ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
      _customerService = customerService;
    }
    [HttpPut]
    public async Task<ActionResult<string>> LoanUpdate(LoanRequestForm loan)
    {
      await _customerService.Update(loan, HttpContext.Request);
      return Ok();
    }

    [HttpGet("{loanId}")]
    public async Task<ActionResult<LoanRequestForm>> GetLoan(int loanId)
    {
      return Ok(await _customerService.GetLoan(loanId, HttpContext.Request));
    }

    [HttpPost]
    public async Task<ActionResult<string>> LoanRequest(LoanRequestForm loan)
    {
      return Ok(await _customerService.Add(loan, HttpContext.Request));
    }

    [HttpGet]
    public async Task<ActionResult<List<LoanRequestForm>>> GetLoans(int page = 1, int limit = 25)
    {
      var result = await _customerService.GetLoans(HttpContext.Request, page, limit);
      return Ok(new { data = result.Item1, total = result.Item2 });
    }
  }
}
