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
        public int idSale { get; set; }
        public string bill { get; set; } = string.Empty; //factura
        public DateOnly saleDate { get; set; }
        public string dni { get; set; } = string.Empty;
        public string customer { get; set; } = string.Empty; //cliente
        public decimal discount { get; set; }
        public decimal total { get; set; }

        [ForeignKey(nameof(User))]
        public int idUser { get; set; }
        public virtual User? User { get; set; }
        public SaleStatus status { get; set; } = SaleStatus.Annulled;
        public DateOnly? annulledDate { get; set; }
        public string? reason { get; set; } //motivo
        public int? userCancel {  get; set; } //usuario que anula
    }

    public enum SaleStatus
    {
        Active = 1,
        Annulled = 0
    }
}
