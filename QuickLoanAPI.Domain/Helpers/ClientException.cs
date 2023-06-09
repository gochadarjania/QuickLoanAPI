﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLoanAPI.Domain.Helpers
{
  [Serializable]
  public class ClientException : Exception
  {
    public ClientException() { }
    public ClientException(string message) : base(message) { }
    public ClientException(string message, Exception inner) : base(message, inner) { }
  }
}
