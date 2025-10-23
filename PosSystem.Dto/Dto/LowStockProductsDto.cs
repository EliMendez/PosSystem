using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class LowStockProductsDto
    {
        public int ProductId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Stock { get; set; }

    }
}
