using System.Collections.Generic;
using SiC.Models;

namespace SiC.DTOs
{
    public class MaterialDTO
    {

        public int MaterialId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<FinishingDTO> finishesDTO { get; set; }

        public MaterialDTO()
        {

        }

    }
}