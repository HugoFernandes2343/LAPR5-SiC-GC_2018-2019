using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class CollectionProduct
    {
        public int CollectionId{get;set;}
        public virtual Collection Collection {get;set;}
        public int ProdutctId { get;set;}
        public virtual Product Product {get; set;}

    }
}