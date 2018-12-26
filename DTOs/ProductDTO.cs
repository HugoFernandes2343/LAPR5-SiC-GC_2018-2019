using System.Collections.Generic;
using SiC.Models;

namespace SiC.DTOs
{
    public class ProductDTO
    {

        public int ProductId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<DimensionDTO> dimensions { get; set; }
        public List<MaterialDTO> materials { get; set; }
        public CategoryDTO category { get; set; }

        public ProductDTO()
        {

        }
    }
}