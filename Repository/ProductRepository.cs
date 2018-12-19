using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Controller;
using SiC.DTO;
using SiC.Model;

namespace SiC.Persistence
{
    public class ProductRepository : Repository<Product, PostProductDTO>
    {
        private PersistenceContext context;
        private DimensionRepository dimensionRepository;
        private RestrictionRepository restrictionRepository;

        public ProductRepository(PersistenceContext context)
        {
            this.context = context;
            this.dimensionRepository = new DimensionRepository(context);
        }

        public async Task<Product> Add(PostProductDTO dto)
        {
            Product product = new Product();

            if (dto.ParentId == -1)
            {
                //attributes
                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Price = dto.Price;
                if (dto.MaxOccupation != null) product.MaxOccupation = dto.MaxOccupation.Value;
                if (dto.MinOccupation != null) product.MinOccupation = dto.MinOccupation.Value;

                //Ids
                product.DimensionId = dto.DimensionId;
                product.RestrictionId = dto.RestrictionId;
                product.CategoryId = dto.CategoryId;

                context.products.Add(product);
                await context.SaveChangesAsync();
            }
            else
            {
                Product parentProduct = context.products
                                            .Include(x => x.Components)
                                            .Where(x => x.Id == dto.ParentId).First();

                var childDimension = await dimensionRepository.FindById(dto.DimensionId);
                var parentDimension = await dimensionRepository.FindById(parentProduct.DimensionId);

                if (childDimension == null) return null;
                if (parentDimension == null) return null;

                product.Dimension = childDimension;
                parentProduct.Dimension = parentDimension;

                if (!Restriction.ProductFits(parentProduct, product)) return null;


                //attributes
                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Price = dto.Price;


                //Ids
                product.DimensionId = dto.DimensionId;
                product.RestrictionId = dto.RestrictionId;
                product.CategoryId = dto.CategoryId;


                parentProduct.Components.Add(product);

                await context.SaveChangesAsync();

            }
            return product;
        }

        public async Task<PostProductDTO> AddComponent(long ParentId, long ChildId)
        {
            var parent = await this.FindById(ParentId);
            var child = await this.FindById(ChildId);

            if (!(Restriction.ProductFits(parent, child))) return null;

            PostProductDTO p = new PostProductDTO();


            p.Name = child.Name;
            p.Description = child.Description;
            p.Price = child.Price;
            p.MaxOccupation = child.MaxOccupation;
            p.MinOccupation = child.MinOccupation;
            p.DimensionId = child.DimensionId;
            p.CategoryId = child.CategoryId;
            p.RestrictionId = child.RestrictionId.Value;
            p.ParentId = (long?)ParentId;

            this.Add(p);

            return p;
        }

        public async Task<Product> Edit(long id, PostProductDTO dto)
        {
            var product = await context.products.FindAsync(id);

            if (product == null)
            {
                return null;
            }
            else
            {
                if (dto.ParentId == -1)
                {

                    //attributes
                    product.Name = dto.Name;
                    product.Description = dto.Description;
                    product.Price = dto.Price;

                    if (dto.MaxOccupation != null) product.MaxOccupation = dto.MaxOccupation.Value;
                    if (dto.MinOccupation != null) product.MinOccupation = dto.MinOccupation.Value;

                    //Ids
                    product.DimensionId = dto.DimensionId;
                    product.RestrictionId = dto.RestrictionId;
                    product.CategoryId = dto.CategoryId;

                }
                else if (dto.ParentId == null)
                {
                    //attributes
                    product.Name = dto.Name;
                    product.Description = dto.Description;
                    product.Price = dto.Price;

                    if (dto.MaxOccupation != null) product.MaxOccupation = dto.MaxOccupation.Value;
                    if (dto.MinOccupation != null) product.MinOccupation = dto.MinOccupation.Value;

                    //Ids
                    product.DimensionId = dto.DimensionId;
                    product.RestrictionId = dto.RestrictionId;
                    product.CategoryId = dto.CategoryId;
                    product.ParentId = null;
                }
                else
                {
                    //attributes
                    product.Name = dto.Name;
                    product.Description = dto.Description;
                    product.Price = dto.Price;

                    //Ids
                    product.DimensionId = dto.DimensionId;
                    product.RestrictionId = dto.RestrictionId;
                    product.CategoryId = dto.CategoryId;
                    product.ParentId = dto.ParentId;
                }

                context.Entry(product).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return product;

            }
        }


        public async Task<List<Product>> FindAllAsync()
        {
            List<Product> products = await context.products.Include(x => x.Components)
                                    .Where(x => x.ParentId == null)
                                    .Select(f => new Product
                                    {
                                        Id = f.Id,
                                        Name = f.Name,
                                        Description = f.Description,
                                        Price = f.Price,
                                        ParentId = f.ParentId,
                                        MaxOccupation = f.MaxOccupation,
                                        MinOccupation = f.MinOccupation,
                                        Components = f.Components,
                                        DimensionId = f.DimensionId,
                                        CategoryId = f.CategoryId,
                                        RestrictionId = f.RestrictionId
                                    }).ToListAsync();

            foreach (Product p in products)
            {
                List<ProductMaterial> materials = await context.productMaterials.Where(m => m.ProductId == p.Id).ToListAsync();

                foreach (ProductMaterial pm in materials)
                {
                    var material = await context.materials.FindAsync(pm.MaterialId);

                    List<MaterialFinish> finishes = await context.materialFinishes.Where(f => f.MaterialId == material.Id).ToListAsync();

                    foreach (MaterialFinish mf in finishes)
                    {
                        var finish = await context.finishes.FindAsync(mf.FinishId);
                        if (!material.Finishes.Contains(finish)) material.Finishes.Add(finish);
                    }

                    p.Materials.Add(material);
                }

                foreach (Product pChild in p.Components)
                {
                    List<ProductMaterial> materialsChild = await context.productMaterials.Where(m => m.ProductId == pChild.Id).ToListAsync();

                    foreach (ProductMaterial pm in materialsChild)
                    {
                        var material = await context.materials.FindAsync(pm.MaterialId);

                        List<MaterialFinish> finishes = await context.materialFinishes.Where(f => f.MaterialId == material.Id).ToListAsync();

                        foreach (MaterialFinish mf in finishes)
                        {
                            var finish = await context.finishes.FindAsync(mf.FinishId);
                            if (!material.Finishes.Contains(finish)) material.Finishes.Add(finish);
                        }

                        pChild.Materials.Add(material);
                        pChild.Dimension = await context.dimensions.FindAsync(p.DimensionId);
                        pChild.Dimension.Height = await context.measures.FindAsync(pChild.Dimension.HeightId);
                        pChild.Dimension.Width = await context.measures.FindAsync(pChild.Dimension.WidthId);
                        pChild.Dimension.Depth = await context.measures.FindAsync(pChild.Dimension.DepthId);
                    }
                }
                p.Dimension = await context.dimensions.FindAsync(p.DimensionId);
                p.Dimension.Height = await context.measures.FindAsync(p.Dimension.HeightId);
                p.Dimension.Width = await context.measures.FindAsync(p.Dimension.WidthId);
                p.Dimension.Depth = await context.measures.FindAsync(p.Dimension.DepthId);
                p.Category = await context.categories.FindAsync(p.CategoryId);
                p.Occupation = Restriction.OccupationPercentage(p);
            }

            return products;
        }

        public async Task<Product> FindById(long id)
        {
            var product = await GetProduct(id);

            if (product == null) return null;

            return product;
        }

        public async Task<Product> Remove(long id)
        {
            var product = await context.products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            context.Entry(product).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> FindChildProduct(long id)
        {
            return await context.products.Where(y => y.Id == id)
                                                .Select(y => new Product
                                                {
                                                    ParentId = y.ParentId
                                                }).FirstOrDefaultAsync();
        }

        public async Task<Product> FindParentProduct(Product childProd)
        {
            Product product = await context.products.Where(y => y.Id == childProd.ParentId)
                                                        .Select(y => new Product
                                                        {
                                                            Id = y.Id,
                                                            Name = y.Name,
                                                            Description = y.Description,
                                                            Price = y.Price,
                                                            ParentId = y.ParentId,
                                                            MaxOccupation = y.MaxOccupation,
                                                            MinOccupation = y.MinOccupation,
                                                            DimensionId = y.DimensionId,
                                                            Dimension = y.Dimension,
                                                            CategoryId = y.CategoryId,
                                                            Category = y.Category,
                                                            RestrictionId = y.RestrictionId,
                                                            Restriction = y.Restriction
                                                        }).FirstAsync();

            List<ProductMaterial> materials = await context.productMaterials.Where(m => m.ProductId == product.Id).ToListAsync();
            foreach (ProductMaterial pm in materials)
            {
                var material = await context.materials.FindAsync(pm.MaterialId);
                List<MaterialFinish> finishes = await context.materialFinishes.Where(f => f.FinishId == material.Id).ToListAsync();
                foreach (MaterialFinish mf in finishes)
                {
                    var finish = await context.finishes.FindAsync(mf.FinishId);
                    material.Finishes.Add(finish);
                }
                product.Materials.Add(material);
            }



            return product;
        }

        public async Task<Product> FindProductByName(string name)
        {
            Product product = await context.products.Include(y => y.Components)
                                                .Where(y => y.Name.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) != -1)
                                                .Select(y => new Product
                                                {
                                                    Id = y.Id,
                                                    Name = y.Name,
                                                    Description = y.Description,
                                                    Price = y.Price,
                                                    ParentId = y.ParentId,
                                                    ParentProduct = y.ParentProduct,
                                                    MaxOccupation = y.MaxOccupation,
                                                    MinOccupation = y.MinOccupation,
                                                    Components = y.Components,
                                                    DimensionId = y.DimensionId,
                                                    Dimension = y.Dimension,
                                                    CategoryId = y.CategoryId,
                                                    Category = y.Category,
                                                    RestrictionId = y.RestrictionId,
                                                    Restriction = y.Restriction
                                                }).FirstAsync();

            List<ProductMaterial> materials = await context.productMaterials.Where(m => m.ProductId == product.Id).ToListAsync();
            foreach (ProductMaterial pm in materials)
            {
                var material = await context.materials.FindAsync(pm.MaterialId);
                List<MaterialFinish> finishes = await context.materialFinishes.Where(f => f.FinishId == material.Id).ToListAsync();
                foreach (MaterialFinish mf in finishes)
                {
                    var finish = await context.finishes.FindAsync(mf.FinishId);
                    material.Finishes.Add(finish);
                }
                product.Materials.Add(material);
            }

            return product;
        }



        public async Task<Product> GetProduct(long Id)
        {
            Product dbProd = await context.products.Include(y => y.Components)
                                              .Where(y => y.Id == Id)
                                              .Select(y => new Product
                                              {
                                                  Id = y.Id,
                                                  Name = y.Name,
                                                  Description = y.Description,
                                                  Price = y.Price,
                                                  ParentId = y.ParentId,
                                                  ParentProduct = y.ParentProduct,
                                                  MaxOccupation = y.MaxOccupation,
                                                  MinOccupation = y.MinOccupation,
                                                  Components = y.Components,
                                                  DimensionId = y.DimensionId,
                                                  Dimension = y.Dimension,
                                                  CategoryId = y.CategoryId,
                                                  Category = y.Category,
                                                  RestrictionId = y.RestrictionId,
                                                  Restriction = y.Restriction
                                              }).FirstOrDefaultAsync();

            if (dbProd == null) return null;

            List<ProductMaterial> materials = await context.productMaterials.Where(m => m.ProductId == dbProd.Id).ToListAsync();

            foreach (ProductMaterial pm in materials)
            {
                var material = await context.materials.FindAsync(pm.MaterialId);

                List<MaterialFinish> finishes = await context.materialFinishes.Where(f => f.MaterialId == material.Id).ToListAsync();

                foreach (MaterialFinish mf in finishes)
                {
                    var finish = await context.finishes.FindAsync(mf.FinishId);
                    if (!material.Finishes.Contains(finish)) material.Finishes.Add(finish);
                }

                dbProd.Materials.Add(material);
            }

            foreach (Product p in dbProd.Components)
            {
                List<ProductMaterial> materialsChild = await context.productMaterials.Where(m => m.ProductId == p.Id).ToListAsync();

                foreach (ProductMaterial pm in materialsChild)
                {
                    var material = await context.materials.FindAsync(pm.MaterialId);

                    List<MaterialFinish> finishes = await context.materialFinishes.Where(f => f.MaterialId == material.Id).ToListAsync();

                    foreach (MaterialFinish mf in finishes)
                    {
                        var finish = await context.finishes.FindAsync(mf.FinishId);
                        if (!material.Finishes.Contains(finish)) material.Finishes.Add(finish);
                    }

                    p.Materials.Add(material);
                }

                p.Dimension = await context.dimensions.FindAsync(p.DimensionId);
                p.Dimension.Height = await context.measures.FindAsync(p.Dimension.HeightId);
                p.Dimension.Width = await context.measures.FindAsync(p.Dimension.WidthId);
                p.Dimension.Depth = await context.measures.FindAsync(p.Dimension.DepthId);
                p.Category = await context.categories.FindAsync(p.CategoryId);
            }

            dbProd.Dimension = await context.dimensions.FindAsync(dbProd.DimensionId);
            dbProd.Dimension.Height = await context.measures.FindAsync(dbProd.Dimension.HeightId);
            dbProd.Dimension.Width = await context.measures.FindAsync(dbProd.Dimension.WidthId);
            dbProd.Dimension.Depth = await context.measures.FindAsync(dbProd.Dimension.DepthId);
            dbProd.Occupation = Restriction.OccupationPercentage(dbProd);

            List<ChildMaterialRestriction> childMaterialRestrictions =
                await context.childMaterialRestrictions
                .Where(c => c.RestrictionId == dbProd.RestrictionId).ToListAsync();

            List<Material> materialDb = await context.materials.ToListAsync();

            List<Material> childMaterials = new List<Material>();

            foreach (ChildMaterialRestriction c in childMaterialRestrictions)
            {
                foreach (Material material in materialDb)
                {

                    if (c.MaterialId == material.Id)
                    {
                        childMaterials.Add(material);
                    }

                }
            }

            dbProd.Restriction.childMaterialRestrictions = childMaterials;

            return dbProd;
        }

        /** Gets all the products in the database */
        private List<Product> GetAll(List<Product> list)
        {
            int z = 0;
            List<Product> lists = new List<Product>();

            if (list.Count > 0) lists.AddRange(list);
            foreach (Product p in list)
            {

                var prodDimension = context.dimensions.Where(d => d.Id == p.DimensionId).FirstOrDefault();
                var prodCategory = new CategoryController(context).GetById(p.CategoryId);
                var prodRestriction = context.restrictions.Where(r => r.Id == p.RestrictionId).FirstOrDefault();

                Product dbProd = context.products.Include(y => y.Components)
                                                  .Where(y => y.Id == p.Id)
                                                  .Select(y => new Product
                                                  {
                                                      Id = y.Id,
                                                      Name = y.Name,
                                                      Description = y.Description,
                                                      Price = y.Price,
                                                      ParentId = y.ParentId,
                                                      MaxOccupation = y.MaxOccupation,
                                                      MinOccupation = y.MinOccupation,
                                                      Components = y.Components,
                                                      DimensionId = y.DimensionId,
                                                      CategoryId = y.CategoryId,
                                                      RestrictionId = y.RestrictionId
                                                  }).First();

                if (dbProd.Components == null)
                {
                    z++;
                    continue;
                }

                List<Product> sub = dbProd.Components.ToList();
                dbProd.Components = GetAll(sub);
                dbProd.Dimension = prodDimension;
                dbProd.Category = (Category)prodCategory.Result;
                dbProd.Restriction = prodRestriction;
                lists[z] = dbProd;
                z++;
            }
            return lists;
        }


        public IEnumerable<Product> FindAll()
        {
            throw new System.NotImplementedException();
        }

    }
}