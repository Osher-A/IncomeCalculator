using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeCalculator.Shared.DTO
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
