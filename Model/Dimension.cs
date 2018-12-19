using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Model
{
    public class Dimension
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long? WidthId { get; set; }
        public virtual Measure Width { get; set; }

        public long? HeightId { get; set; }
        public virtual Measure Height { get; set; }

        public long? DepthId { get; set; }
        public virtual Measure Depth { get; set; }
    }
}