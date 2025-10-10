using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class CancelSaleDto
    {
        public string Reason { get; set; } = string.Empty; //motivo
        public int UserId { get; set; }
    }
}
