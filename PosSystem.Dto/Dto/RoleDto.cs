using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class RoleDto
    {
        public int idRole { get; set; }
        public string description { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
    }
}
