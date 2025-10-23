using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class LastWeekIncomeTotalDto
    {
        public DateTime SaleDate { get; set; }
        public decimal TotalIncomes { get; set; }
    }
}
