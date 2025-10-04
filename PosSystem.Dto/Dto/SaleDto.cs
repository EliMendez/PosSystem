using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class SaleDto
    {
        public int idSale { get; set; }
        public string bill { get; set; } = string.Empty; //factura
        public DateOnly saleDate { get; set; }
        public string dni { get; set; } = string.Empty;
        public string customer { get; set; } = string.Empty; //cliente
        public decimal discount { get; set; }
        public decimal total { get; set; }
        public int idUser { get; set; }
        public SaleStatus status { get; set; } = SaleStatus.Active;
        public DateOnly? annulledDate { get; set; }
        public string? reason { get; set; } //motivo
        public int? userCancel { get; set; } //usuario que anula
        public virtual ICollection<SaleDetailDto>? SaleDetails { get; set; }
    }
}
