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
        public int SaleId { get; set; }
        public string? Bill { get; set; } //factura
        public DateOnly SaleDate { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty; //cliente
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }
        public SaleStatus Status { get; set; } = SaleStatus.Active;
        public DateOnly? AnnulledDate { get; set; }
        public string? Reason { get; set; } //motivo
        public int? UserCancel { get; set; } //usuario que anula
        public List<SaleDetailDto>? SaleDetails { get; set; }
    }
}
