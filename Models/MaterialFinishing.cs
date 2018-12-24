namespace SiC.Models
{
    public class MaterialFinishing
    {
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }
        public int FinishingId { get; set; }
        public virtual Finishing Finishing { get; set; }
    }
}