using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Dto.Dto
{
    public class UserDto
    {
        public int idUser { get; set; }
        public string name { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;
        public int idRole { get; set; }
        public string roleDescription { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
    }
}
