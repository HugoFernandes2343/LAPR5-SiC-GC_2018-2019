using SiC.Models;

namespace SiC.DTOs
{
    public class RestrictionDTO
    {
        public int RestrictionId { get; set; }
        public string description { get; set;}
        public CombinationDTO combination { get; set; }


    }
}