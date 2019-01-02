using System.Collections.Generic;
using SiC.Models;

namespace SiC.DTOs
{
    public class FinishingDTO
    {
        public int finishingId {get;set;}
        public string name {get;set;}
        public string description {get;set;}

        public List<PriceDTO> prices { get; set; }

        public FinishingDTO()
        {

        }

    }
    
}