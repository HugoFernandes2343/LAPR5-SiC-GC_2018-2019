using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Model
{
    public class Category
    {

        public Category()
        {
            SubCategories = new HashSet<Category>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }

        public long? ParentID { get; set; }
        public virtual Category ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}