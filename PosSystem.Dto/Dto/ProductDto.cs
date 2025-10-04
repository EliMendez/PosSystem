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
        public int idProduct { get; set; }
        public string barcode { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int idCategory { get; set; }
        public string categoryDescription { get; set; } = string.Empty;
        public decimal salePrice { get; set; }
        public int stock { get; set; }
        public int minimumStock { get; set; }
        public string status { get; set; } = string.Empty;
    }
}
