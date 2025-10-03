using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Model.Model
{
    public class DocumentNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idDocumentNumber { get; set; }
        public string document { get; set; } = string.Empty;
        public DateTime creationDate { get; private set; }
    }
}
