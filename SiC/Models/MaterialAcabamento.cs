using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class MaterialAcabamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Nome { get; set; }
        public long MaterialId { get; set; }
    }
}