using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Collection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollectionId { get; set; }

        public virtual List<Product> products { get; set; }

        public string collectionName { get; set; }
        public string aestheticParameter { get; set; }

        public Collection()
        {
        }
    }
}