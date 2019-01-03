using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Collection
    {

        public int CollectionId { get; set; }

        public virtual ICollection<CollectionProduct> CollectionProducts { get; set; }

        public string collectionName { get; set; }
        public string aestheticParameter { get; set; }

    }
}