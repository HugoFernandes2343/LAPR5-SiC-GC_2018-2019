namespace SiC.Models
{
    public class RestrictionMat : Restriction
    {
        public virtual Material containingMaterial { get; set; }

        public virtual Material containedMaterial { get; set; }
    }
}