using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Repository
{
    public class ProductRepository : Repository<Product, ProductDTO>
    {
        private SiCContext context;

        public ProductRepository(SiCContext context)
        {
            this.context = context;
        }
        public async Task<Product> Add(ProductDTO dto)
        {
            if (context.Product.Any(p => p.name == dto.name)) return null;

            List<Dimension> dims = new List<Dimension>();
            List<Material> mats = new List<Material>();

            string catName = dto.category.name.Split(";").Last();
            var category = context.Category.SingleOrDefault(c => c.name == catName);

            if (category == null) return null;

            Product product = new Product();
            product.name = dto.name;
            product.description = dto.description;
            product.category = category;
            product.dimensions = dims;

            context.Product.Add(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Edit(int id, ProductDTO dto)
        {

            var product = await context.Product.FindAsync(id);

            if (product == null) return null;

            if (context.Product.Any(p => p.name == dto.name && p.ProductId != id)) return null;

            product.name = dto.name;
            product.description = dto.description;

            context.Entry(product).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return product;
        }

        public IEnumerable<Product> FindAll()
        {
            return context.Product;
        }

        public async Task<Product> FindById(int id)
        {
            return await context.Product.FindAsync(id);
        }

        public async Task<Product> Remove(int id)
        {
            var product = await context.Product.FindAsync(id);

            if (product == null) return null;

            foreach (ProductMaterial pm in product.ProductMaterials)
            {
                context.ProductMaterial.Remove(pm);
            }

            foreach (Dimension dim in product.dimensions)
            {
                context.Dimension.Remove(dim);
            }

            List<Restriction> res_elim = new List<Restriction>();
            var res_elim_query = from r in context.Restriction where r.combination.containingProduct.ProductId == id || r.combination.containedProduct.ProductId == id select r;
            res_elim = res_elim_query.ToList();

            List<Combination> comb_elim = new List<Combination>();
            var comb_elim_query = from c in context.Combination where c.containingProduct.ProductId == id || c.containedProduct.ProductId == id select c;
            comb_elim = comb_elim_query.ToList();

            foreach (Restriction res in res_elim)
            {
                context.Restriction.Remove(res);
            }

            foreach (Combination comb in comb_elim)
            {
                context.Combination.Remove(comb);
            }

            context.Product.Remove(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> Parts(int id)
        {
            List<Product> products = new List<Product>();
            var product = await context.Product.FindAsync(id);

            if (product == null) return null;

            var query = from c in context.Combination where c.containingProduct.ProductId == product.ProductId select c.containedProduct;
            products = query.ToList();

            return products;
        }

        public async Task<List<Product>> PartOff(int id)
        {
            List<Product> products = new List<Product>();
            var product = await context.Product.FindAsync(id);

            if (product == null) return null;

            var query = from c in context.Combination where c.containedProduct.ProductId == product.ProductId select c.containingProduct;
            products = query.ToList();

            return products;
        }

        public async Task<Product> FindByName(string name)
        {
            return await context.Product.SingleOrDefaultAsync(p => p.name == name);
        }

        public async Task<List<Restriction>> Restricitons(int id)
        {
            var product = await context.Product.FindAsync(id);

            if (product == null) return null;

            List<Restriction> restrictions = new List<Restriction>();
            var query = from r in context.Restriction where r.combination.containingProduct.ProductId == id || r.combination.containedProduct.ProductId == id select r;
            restrictions = query.ToList();

            return restrictions;
        }

        internal async Task<Product> AddProductMaterial(int id, int idm)
        {
            var product = await context.Product.FindAsync(id);
            var material = await context.Material.FindAsync(idm);

            if (product == null || material == null) return null;

            if (context.ProductMaterial.Any(pms => pms.ProductId == id && pms.MaterialId == idm)) return null;

            ProductMaterial pm = new ProductMaterial();
            pm.ProductId = product.ProductId;
            pm.Product = product;
            pm.MaterialId = material.MaterialId;
            pm.Material = material;

            product.ProductMaterials.Add(pm);
            material.ProductMaterials.Add(pm);

            context.Entry(material).State = EntityState.Modified;
            context.Entry(product).State = EntityState.Modified;
            context.ProductMaterial.Add(pm);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return product;
        }

        internal async Task<Product> AddDimension(int id, int idd)
        {
            var product = await context.Product.FindAsync(id);
            var dimension = await context.Dimension.FindAsync(idd);

            if (product == null || dimension == null) return null;

            if (product.dimensions.Contains(dimension)) return null;

            product.dimensions.Add(dimension);

            context.Entry(product).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return product;

        }

        internal async Task<Product> RemoveProductMaterial(int id, int idm)
        {
            var product = await context.Product.FindAsync(id);
            var material = await context.Material.FindAsync(idm);
            ProductMaterial pms = null;

            if (product == null || material == null) return null;

            foreach(ProductMaterial pm in product.ProductMaterials){
                if(pm.MaterialId == idm){
                    pms = pm;
                }
            }

            if (pms == null) return null;

            context.ProductMaterial.Remove(pms);
            await context.SaveChangesAsync();

            return product;
        }
    }
}