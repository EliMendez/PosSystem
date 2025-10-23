using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class SellingMoreProductsDto
    {
        public int ProductId { get; set; }
        public string Product { get; set; } = string.Empty;
        public int SoldQuantity { get; set; }

    }
}
