using SiC.Models;

namespace SiC.DTOs
{
    public class CombinationDTO
    {
        public int CombinationId;

        public string containingProduct { get; set; }

        public string containedProduct { get; set; }

        public bool required { get; set; }

        public CombinationDTO()
        {

        }

    }
}