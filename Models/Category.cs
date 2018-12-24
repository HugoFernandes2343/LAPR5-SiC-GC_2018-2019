using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Category
    {

        public int CategoryId { get; set; }
        public string name { get; set; }
        public virtual Category parent { get; set; }

        public Category(string name, Category parent)
        {
            this.parent = parent;
            this.name = name;
        }

        public Category(string name)
        {
            this.name = name;
        }

        public Category()
        {

        }

        public List<Category> allFathers(){
            if(parent==null){
                return new List<Category>();
            }

            return allFathersSearch(new List<Category>(), this.parent);
        }

        private List<Category> allFathersSearch(List<Category> categories, Category cat){
            categories.Add(cat);
            if(cat.parent==null){
                return categories;
            }
            return allFathersSearch(categories, cat.parent);
        }
    }
}