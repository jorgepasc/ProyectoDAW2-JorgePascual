using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpecialOlympics.Models
{
    [Table("Documentos")]
    public class Documento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int IdDocumento { get; set; }

        [StringLength(150)]
        public string Nombre { get; set; }

        public byte[] Value { get; set; }

    }
}
