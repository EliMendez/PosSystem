using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Model.Model
{
    public class Business
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessId { get; set; }
        public string Ruc { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty; // razón social
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty; // dirección
        public string Owner { get; set; } = string.Empty; // propietario
        public decimal Discount { get; set; } // descuento
        public DateTime CreationDate { get; private set; }
    }
}
