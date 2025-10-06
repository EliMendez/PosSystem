using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }
        public int MinimumStock { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
