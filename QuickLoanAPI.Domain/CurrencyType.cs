using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Domain
{
  public class CurrencyType
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
