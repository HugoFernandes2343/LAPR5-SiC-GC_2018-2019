using System.Collections.Generic;

namespace SiC.DTOs
{
    public class CollectionDTO
    {
        public int CollectionId { get; set; }
        public List<ProductDTO> products { get; set; }
        public string collectionName { get; set; }
        public string aestheticParameter { get; set; }

        public CollectionDTO()
        {
        }
    }
}