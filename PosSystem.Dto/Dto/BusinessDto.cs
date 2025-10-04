using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class BusinessDto
    {
        public int idBusiness { get; set; }
        public string ruc { get; set; } = string.Empty;
        public string companyName { get; set; } = string.Empty; // razón social
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty; // dirección
        public string owner { get; set; } = string.Empty; // propietario
        public decimal discount { get; set; } // descuento
    }
}
