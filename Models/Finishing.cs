using System.Collections.Generic;

namespace SiC.Models
{
    public class Finishing
    {

        public int FinishingId { get; set; }

        public string name { get; set; }

        public virtual ICollection<MaterialFinishing> MaterialFinishings { get; set; }

        public Finishing()
        {

        }

    }
}