using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Model.Model
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCategory { get; set; }
        public string description { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public DateTime creationDate { get; private set; }
    }
}
