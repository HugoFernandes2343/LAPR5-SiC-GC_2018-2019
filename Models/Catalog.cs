using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Catalog
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CatalogId { get; set; }
        public string CatalogName {get; set;}
        public string CatalogDescription {get; set;}
        public virtual ICollection<CatalogProduct> CatalogProducts { get; set; }
        
        public string Date;

        public Catalog()
        {

        }

    }
}