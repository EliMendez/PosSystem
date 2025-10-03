using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Model.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProduct { get; set; }
        public string barcode { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;

        [ForeignKey(nameof(Category))]
        public int idCategory { get; set; }
        public virtual Category? Category { get; set; }
        public decimal salePrice { get; set; }
        public int stock { get; set; }
        public int minimumStock { get; set; }
        public string status { get; set; } = string.Empty;
        public DateTime creationDate { get; private set; }
    }
}
