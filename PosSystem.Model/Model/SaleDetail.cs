using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Model.Model
{
    public class SaleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idSaleDetail { get; set; }

        [ForeignKey(nameof(Sale))]
        public int idSale { get; set; }
        public virtual Sale? Sale { get; set; }

        [ForeignKey(nameof(Product))]
        public int idProduct { get; set; }
        public virtual Product? Product { get; set; }
        public string productName { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int count { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }
    }
}
