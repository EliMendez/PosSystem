using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Model.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUser { get; set; }
        public string name { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;

        [ForeignKey(nameof(Role))]
        public int idRole { get; set; }
        public virtual Role? Role { get; set; }
        public string phone { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public DateTime creationDate { get; private set; }
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
