using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class SaleDetailDto
    {
        public int idSaleDetail { get; set; }
        public int idSale { get; set; }
        public int idProduct { get; set; }
        public string productName { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int count { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }
    }
}
