using SiC.Models;

namespace SiC.DTOs
{
    public class CategoryDTO
    {
        public string name { get; set; }
        public int CategoryId { get; set; }
        public string description { get; set; }


        public CategoryDTO(string name, Category category)
        {
            this.name = convertToTaxonomy(name, category);
            this.CategoryId = category.CategoryId;
        }

        public CategoryDTO()
        {

        }

        public string convertToTaxonomy(string name, Category category)
        {
            if (category.parent == null)
            {
                return name;
            }
            name = category.parent.name + ";" + name;
            return convertToTaxonomy(name, category.parent);
        }
        public Category convertToCategory()
        {
            string[] names = name.Split(";");
            Category parentOfparent = new Category(names[0]);
            Category cat = new Category(names[1], parentOfparent);
            for (int i = 2; i < names.Length; i++)
            {
                Category cat1 = cat;
                cat = new Category(names[i], cat);
            }
            return cat;
        }
    }
}