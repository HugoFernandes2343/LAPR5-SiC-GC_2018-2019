using System.Collections.Generic;
using SiC.Models;

namespace SiC.DTOs
{
    public class FactoryDTO
    {
        public int FactoryId { get; set; }
        public string Description { get; set; }
        public virtual CityDTO City { get; set; }

        public FactoryDTO()
        {

        }

    }
}