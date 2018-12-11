using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Material
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id{ get; set;}

        public string Descricao{ get; set;}
    }
}