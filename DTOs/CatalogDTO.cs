using System.Collections.Generic;

namespace SiC.DTOs
{
    public class CatalogDTO
    {
        public int CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string CatalogDescription { get; set; }
        public List<ProductDTO> products { get; set; }
        public string Date { get; set; }

        public CatalogDTO()
        {

        }
    }


}