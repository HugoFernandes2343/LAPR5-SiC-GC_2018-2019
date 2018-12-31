using System.Collections.Generic;
using SiC.Models;

namespace SiC.DTOs
{
    public class CityDTO
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public CityDTO()
        {

        }

    }
}