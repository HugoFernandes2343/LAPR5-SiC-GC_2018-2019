using System.Collections.Generic;
using SiC.Models;

namespace SiC.DTOs
{
    public class MaterialDTO
    {

        public int MaterialId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<FinishingDTO> finishes { get; set; }

        public List<PriceDTO> prices { get; set; }

        public MaterialDTO()
        {

        }

    }
}