using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Model.Model
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        public string Bill { get; set; } = string.Empty; //factura
        public DateOnly SaleDate { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty; //cliente
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public SaleStatus Status { get; set; } = SaleStatus.Active;
        public DateOnly? AnnulledDate { get; set; }
        public string? Reason { get; set; } //motivo
        public int? UserCancel {  get; set; } //usuario que anula
        public virtual ICollection<SaleDetail>? SaleDetails { get; set; }
    }

    public enum SaleStatus
    {
        Active = 1,
        Annulled = 0
    }
}
