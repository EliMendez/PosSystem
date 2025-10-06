using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class BusinessDto
    {
        public int BusinessId { get; set; }
        public string Ruc { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty; // razón social
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty; // dirección
        public string Owner { get; set; } = string.Empty; // propietario
        public decimal Discount { get; set; } // descuento
    }
}
