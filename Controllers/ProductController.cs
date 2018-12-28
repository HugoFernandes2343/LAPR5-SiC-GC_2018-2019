using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.DTOs;
using SiC.Models;
using SiC.Repository;

namespace SiC.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository productRepository;

        public ProductController(SiCContext context)
        {
            productRepository = new ProductRepository(context);
        }

        // GET: api/Product
        [HttpGet]
        public IEnumerable<ProductDTO> GetProduct()
        {
            List<ProductDTO> dtos = new List<ProductDTO>();
            foreach (Product product in productRepository.FindAll())
            {
                ProductDTO dto = new ProductDTO();
                CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
                dto.ProductId = product.ProductId;
                dto.name = product.name;
                dto.description = product.description;
                dto.dimensions = new List<DimensionDTO>();
                dto.materials = new List<MaterialDTO>();
                foreach (ProductMaterial pm in product.ProductMaterials)
                {
                    MaterialDTO mat_dto = new MaterialDTO();
                    mat_dto.name = pm.Material.name;
                    mat_dto.description = pm.Material.description;
                    mat_dto.MaterialId = pm.Material.MaterialId;
                    mat_dto.finishes = new List<FinishingDTO>();

                    foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                    {
                        FinishingDTO fdto = new FinishingDTO();
                        fdto.finishingId = mf.Finishing.FinishingId;
                        fdto.description = mf.Finishing.description;
                        fdto.name = mf.Finishing.name;
                        mat_dto.finishes.Add(fdto);
                    }

                    dto.materials.Add(mat_dto);
                }

                foreach (Dimension dimension in product.dimensions)
                {
                    DimensionDTO dim_dto = new DimensionDTO();
                    dim_dto.Depth = new MeasureDTO();
                    dim_dto.Height = new MeasureDTO();
                    dim_dto.Width = new MeasureDTO();
                    dim_dto.DimensionId = dimension.DimensionId;
                    dim_dto.Depth.Id = dimension.Depth.MeasureId;
                    dim_dto.Depth.Value = dimension.Depth.Value;
                    dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                    dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                    dim_dto.Height.Id = dimension.Height.MeasureId;
                    dim_dto.Height.Value = dimension.Height.Value;
                    dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                    dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                    dim_dto.Width.Id = dimension.Width.MeasureId;
                    dim_dto.Width.Value = dimension.Width.Value;
                    dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                    dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                    dto.dimensions.Add(dim_dto);
                }
                dtos.Add(dto);
            }
            return dtos;
        }

        // GET: api/Product/id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productRepository.FindById(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDTO dto = new ProductDTO();
            CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
            dto.ProductId = product.ProductId;
            dto.name = product.name;
            dto.description = product.description;
            dto.dimensions = new List<DimensionDTO>();
            dto.materials = new List<MaterialDTO>();
            foreach (ProductMaterial pm in product.ProductMaterials)
            {
                MaterialDTO mat_dto = new MaterialDTO();
                mat_dto.name = pm.Material.name;
                mat_dto.description = pm.Material.description;
                mat_dto.MaterialId = pm.Material.MaterialId;
                mat_dto.finishes = new List<FinishingDTO>();

                foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                {
                    FinishingDTO fdto = new FinishingDTO();
                    fdto.finishingId = mf.Finishing.FinishingId;
                    fdto.description = mf.Finishing.description;
                    fdto.name = mf.Finishing.name;
                    mat_dto.finishes.Add(fdto);
                }

                dto.materials.Add(mat_dto);
            }

            foreach (Dimension dimension in product.dimensions)
            {
                DimensionDTO dim_dto = new DimensionDTO();
                dim_dto.Depth = new MeasureDTO();
                dim_dto.Height = new MeasureDTO();
                dim_dto.Width = new MeasureDTO();
                dim_dto.DimensionId = dimension.DimensionId;
                dim_dto.Depth.Id = dimension.Depth.MeasureId;
                dim_dto.Depth.Value = dimension.Depth.Value;
                dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                dim_dto.Height.Id = dimension.Height.MeasureId;
                dim_dto.Height.Value = dimension.Height.Value;
                dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                dim_dto.Width.Id = dimension.Width.MeasureId;
                dim_dto.Width.Value = dimension.Width.Value;
                dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                dto.dimensions.Add(dim_dto);
            }

            return Ok(dto);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productDTO.ProductId)
            {
                return BadRequest();
            }

            var product = await productRepository.Edit(id, productDTO);

            if (product == null)
            {
                return BadRequest();
            }

            ProductDTO dto = new ProductDTO();
            CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
            dto.ProductId = product.ProductId;
            dto.name = product.name;
            dto.description = product.description;
            dto.dimensions = new List<DimensionDTO>();
            dto.materials = new List<MaterialDTO>();
            foreach (ProductMaterial pm in product.ProductMaterials)
            {
                MaterialDTO mat_dto = new MaterialDTO();
                mat_dto.name = pm.Material.name;
                mat_dto.description = pm.Material.description;
                mat_dto.MaterialId = pm.Material.MaterialId;
                mat_dto.finishes = new List<FinishingDTO>();

                foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                {
                    FinishingDTO fdto = new FinishingDTO();
                    fdto.finishingId = mf.Finishing.FinishingId;
                    fdto.description = mf.Finishing.description;
                    fdto.name = mf.Finishing.name;
                    mat_dto.finishes.Add(fdto);
                }

                dto.materials.Add(mat_dto);
            }

            foreach (Dimension dimension in product.dimensions)
            {
                DimensionDTO dim_dto = new DimensionDTO();
                dim_dto.Depth = new MeasureDTO();
                dim_dto.Height = new MeasureDTO();
                dim_dto.Width = new MeasureDTO();
                dim_dto.DimensionId = dimension.DimensionId;
                dim_dto.Depth.Id = dimension.Depth.MeasureId;
                dim_dto.Depth.Value = dimension.Depth.Value;
                dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                dim_dto.Height.Id = dimension.Height.MeasureId;
                dim_dto.Height.Value = dimension.Height.Value;
                dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                dim_dto.Width.Id = dimension.Width.MeasureId;
                dim_dto.Width.Value = dimension.Width.Value;
                dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                dto.dimensions.Add(dim_dto);
            }

            return Ok(dto);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productRepository.Add(productDTO);

            if (product == null)
            {
                return BadRequest();
            }

            ProductDTO dto = new ProductDTO();
            CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
            dto.ProductId = product.ProductId;
            dto.name = product.name;
            dto.description = product.description;
            dto.dimensions = new List<DimensionDTO>();
            dto.materials = new List<MaterialDTO>();
            foreach (ProductMaterial pm in product.ProductMaterials)
            {
                MaterialDTO mat_dto = new MaterialDTO();
                mat_dto.name = pm.Material.name;
                mat_dto.description = pm.Material.description;
                mat_dto.MaterialId = pm.Material.MaterialId;
                mat_dto.finishes = new List<FinishingDTO>();

                foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                {
                    FinishingDTO fdto = new FinishingDTO();
                    fdto.finishingId = mf.Finishing.FinishingId;
                    fdto.description = mf.Finishing.description;
                    fdto.name = mf.Finishing.name;
                    mat_dto.finishes.Add(fdto);
                }

                dto.materials.Add(mat_dto);
            }

            foreach (Dimension dimension in product.dimensions)
            {
                DimensionDTO dim_dto = new DimensionDTO();
                dim_dto.Depth = new MeasureDTO();
                dim_dto.Height = new MeasureDTO();
                dim_dto.Width = new MeasureDTO();
                dim_dto.DimensionId = dimension.DimensionId;
                dim_dto.Depth.Id = dimension.Depth.MeasureId;
                dim_dto.Depth.Value = dimension.Depth.Value;
                dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                dim_dto.Height.Id = dimension.Height.MeasureId;
                dim_dto.Height.Value = dimension.Height.Value;
                dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                dim_dto.Width.Id = dimension.Width.MeasureId;
                dim_dto.Width.Value = dimension.Width.Value;
                dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                dto.dimensions.Add(dim_dto);
            }

            return CreatedAtAction("GetProduct", dto);
        }

        // DELETE: api/Product/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productRepository.Remove(id);

            if (product == null)
            {
                return BadRequest();
            }

            ProductDTO dto = new ProductDTO();
            CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
            dto.ProductId = product.ProductId;
            dto.name = product.name;
            dto.description = product.description;
            dto.dimensions = new List<DimensionDTO>();
            dto.materials = new List<MaterialDTO>();
            foreach (ProductMaterial pm in product.ProductMaterials)
            {
                MaterialDTO mat_dto = new MaterialDTO();
                mat_dto.name = pm.Material.name;
                mat_dto.description = pm.Material.description;
                mat_dto.MaterialId = pm.Material.MaterialId;
                mat_dto.finishes = new List<FinishingDTO>();

                foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                {
                    FinishingDTO fdto = new FinishingDTO();
                    fdto.finishingId = mf.Finishing.FinishingId;
                    fdto.description = mf.Finishing.description;
                    fdto.name = mf.Finishing.name;
                    mat_dto.finishes.Add(fdto);
                }

                dto.materials.Add(mat_dto);
            }

            foreach (Dimension dimension in product.dimensions)
            {
                DimensionDTO dim_dto = new DimensionDTO();
                dim_dto.Depth = new MeasureDTO();
                dim_dto.Height = new MeasureDTO();
                dim_dto.Width = new MeasureDTO();
                dim_dto.DimensionId = dimension.DimensionId;
                dim_dto.Depth.Id = dimension.Depth.MeasureId;
                dim_dto.Depth.Value = dimension.Depth.Value;
                dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                dim_dto.Height.Id = dimension.Height.MeasureId;
                dim_dto.Height.Value = dimension.Height.Value;
                dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                dim_dto.Width.Id = dimension.Width.MeasureId;
                dim_dto.Width.Value = dimension.Width.Value;
                dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                dto.dimensions.Add(dim_dto);
            }

            return Ok(dto);
        }

        // GET: api/Product/id/Parts
        [HttpGet("{id}/Parts")]
        public async Task<IActionResult> GetParts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Product> products = await productRepository.Parts(id);

            if (products == null)
            {
                return BadRequest();
            }

            List<ProductDTO> dtos = new List<ProductDTO>();

            foreach (Product product in products)
            {
                ProductDTO dto = new ProductDTO();
                CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
                dto.ProductId = product.ProductId;
                dto.name = product.name;
                dto.description = product.description;
                dto.dimensions = new List<DimensionDTO>();
                dto.materials = new List<MaterialDTO>();
                foreach (ProductMaterial pm in product.ProductMaterials)
                {
                    MaterialDTO mat_dto = new MaterialDTO();
                    mat_dto.name = pm.Material.name;
                    mat_dto.description = pm.Material.description;
                    mat_dto.MaterialId = pm.Material.MaterialId;
                    mat_dto.finishes = new List<FinishingDTO>();

                    foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                    {
                        FinishingDTO fdto = new FinishingDTO();
                        fdto.finishingId = mf.Finishing.FinishingId;
                        fdto.description = mf.Finishing.description;
                        fdto.name = mf.Finishing.name;
                        mat_dto.finishes.Add(fdto);
                    }

                    dto.materials.Add(mat_dto);
                }

                foreach (Dimension dimension in product.dimensions)
                {
                    DimensionDTO dim_dto = new DimensionDTO();
                    dim_dto.Depth = new MeasureDTO();
                    dim_dto.Height = new MeasureDTO();
                    dim_dto.Width = new MeasureDTO();
                    dim_dto.DimensionId = dimension.DimensionId;
                    dim_dto.Depth.Id = dimension.Depth.MeasureId;
                    dim_dto.Depth.Value = dimension.Depth.Value;
                    dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                    dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                    dim_dto.Height.Id = dimension.Height.MeasureId;
                    dim_dto.Height.Value = dimension.Height.Value;
                    dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                    dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                    dim_dto.Width.Id = dimension.Width.MeasureId;
                    dim_dto.Width.Value = dimension.Width.Value;
                    dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                    dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                    dto.dimensions.Add(dim_dto);
                }

                dtos.Add(dto);
            }


            return Ok(dtos);
        }

        // GET: api/Product/id/PartOff
        [HttpGet("{id}/PartOff")]
        public async Task<IActionResult> GetPartOff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Product> products = await productRepository.Parts(id);

            if (products == null)
            {
                return BadRequest();
            }

            List<ProductDTO> dtos = new List<ProductDTO>();

            foreach (Product product in products)
            {
                ProductDTO dto = new ProductDTO();
                CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
                dto.ProductId = product.ProductId;
                dto.name = product.name;
                dto.description = product.description;
                dto.dimensions = new List<DimensionDTO>();
                dto.materials = new List<MaterialDTO>();
                foreach (ProductMaterial pm in product.ProductMaterials)
                {
                    MaterialDTO mat_dto = new MaterialDTO();
                    mat_dto.name = pm.Material.name;
                    mat_dto.description = pm.Material.description;
                    mat_dto.MaterialId = pm.Material.MaterialId;
                    mat_dto.finishes = new List<FinishingDTO>();

                    foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                    {
                        FinishingDTO fdto = new FinishingDTO();
                        fdto.finishingId = mf.Finishing.FinishingId;
                        fdto.description = mf.Finishing.description;
                        fdto.name = mf.Finishing.name;
                        mat_dto.finishes.Add(fdto);
                    }

                    dto.materials.Add(mat_dto);
                }

                foreach (Dimension dimension in product.dimensions)
                {
                    DimensionDTO dim_dto = new DimensionDTO();
                    dim_dto.Depth = new MeasureDTO();
                    dim_dto.Height = new MeasureDTO();
                    dim_dto.Width = new MeasureDTO();
                    dim_dto.DimensionId = dimension.DimensionId;
                    dim_dto.Depth.Id = dimension.Depth.MeasureId;
                    dim_dto.Depth.Value = dimension.Depth.Value;
                    dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                    dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                    dim_dto.Height.Id = dimension.Height.MeasureId;
                    dim_dto.Height.Value = dimension.Height.Value;
                    dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                    dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                    dim_dto.Width.Id = dimension.Width.MeasureId;
                    dim_dto.Width.Value = dimension.Width.Value;
                    dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                    dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                    dto.dimensions.Add(dim_dto);
                }

                dtos.Add(dto);
            }

            return Ok(dtos);
        }

        // GET: api/Product/Search/{name}
        [HttpGet("Search/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productRepository.FindByName(name);

            if (product == null)
            {
                return NotFound();
            }

            ProductDTO dto = new ProductDTO();
            CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
            dto.ProductId = product.ProductId;
            dto.name = product.name;
            dto.description = product.description;
            dto.dimensions = new List<DimensionDTO>();
            dto.materials = new List<MaterialDTO>();
            foreach (ProductMaterial pm in product.ProductMaterials)
            {
                MaterialDTO mat_dto = new MaterialDTO();
                mat_dto.name = pm.Material.name;
                mat_dto.description = pm.Material.description;
                mat_dto.MaterialId = pm.Material.MaterialId;
                mat_dto.finishes = new List<FinishingDTO>();

                foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                {
                    FinishingDTO fdto = new FinishingDTO();
                    fdto.finishingId = mf.Finishing.FinishingId;
                    fdto.description = mf.Finishing.description;
                    fdto.name = mf.Finishing.name;
                    mat_dto.finishes.Add(fdto);
                }

                dto.materials.Add(mat_dto);
            }

            foreach (Dimension dimension in product.dimensions)
            {
                DimensionDTO dim_dto = new DimensionDTO();
                dim_dto.Depth = new MeasureDTO();
                dim_dto.Height = new MeasureDTO();
                dim_dto.Width = new MeasureDTO();
                dim_dto.DimensionId = dimension.DimensionId;
                dim_dto.Depth.Id = dimension.Depth.MeasureId;
                dim_dto.Depth.Value = dimension.Depth.Value;
                dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                dim_dto.Height.Id = dimension.Height.MeasureId;
                dim_dto.Height.Value = dimension.Height.Value;
                dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                dim_dto.Width.Id = dimension.Width.MeasureId;
                dim_dto.Width.Value = dimension.Width.Value;
                dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                dto.dimensions.Add(dim_dto);
            }

            return Ok(dto);
        }


        // GET: api/Product/id/Restrictions
        [HttpGet("{id}/Restrictions")]
        public async Task<IActionResult> GetRestrictions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Restriction> restrictions = await productRepository.Restricitons(id);

            if (restrictions == null)
            {
                return BadRequest();
            }

            List<RestrictionDTO> dtos = new List<RestrictionDTO>();
            foreach (Restriction restriction in restrictions)
            {
                CombinationDTO combination = new CombinationDTO();
                combination.CombinationId = restriction.combination.CombinationId;
                combination.containedProduct = restriction.combination.containedProduct.name;
                combination.containingProduct = restriction.combination.containingProduct.name;
                combination.required = restriction.combination.required;

                if (restriction is RestrictionDim)
                {
                    RestrictionDim res_dim = (RestrictionDim)restriction;
                    RestrictionDimDTO dto = new RestrictionDimDTO();
                    dto.combination = combination;
                    dto.description = res_dim.description;
                    dto.RestrictionId = res_dim.RestrictionId;
                    dto.x = res_dim.x;
                    dto.y = res_dim.y;
                    dto.z = res_dim.z;
                    dtos.Add(dto);
                }

                if (restriction is RestrictionMat)
                {
                    RestrictionMat res_mat = (RestrictionMat)restriction;
                    RestrictionMatDTO dto = new RestrictionMatDTO();
                    dto.combination = combination;
                    dto.description = res_mat.description;
                    dto.RestrictionId = res_mat.RestrictionId;
                    dto.containingMaterial = res_mat.containingMaterial.name;
                    dto.containedMaterial = res_mat.containedMaterial.name;
                    dtos.Add(dto);
                }

            }

            return Ok(dtos);
        }

        // PUT: api/Product/id/Material/idm
        [HttpPut("{id}/Material/{idm}")]
        public async Task<IActionResult> PutProductMaterial([FromRoute] int id, [FromRoute] int idm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productRepository.AddProductMaterial(id, idm);

            if (product == null)
            {
                return BadRequest();
            }

            ProductDTO dto = new ProductDTO();
            CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
            dto.ProductId = product.ProductId;
            dto.name = product.name;
            dto.description = product.description;
            dto.dimensions = new List<DimensionDTO>();
            dto.materials = new List<MaterialDTO>();
            foreach (ProductMaterial pm in product.ProductMaterials)
            {
                MaterialDTO mat_dto = new MaterialDTO();
                mat_dto.name = pm.Material.name;
                mat_dto.description = pm.Material.description;
                mat_dto.MaterialId = pm.Material.MaterialId;
                mat_dto.finishes = new List<FinishingDTO>();

                foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                {
                    FinishingDTO fdto = new FinishingDTO();
                    fdto.finishingId = mf.Finishing.FinishingId;
                    fdto.description = mf.Finishing.description;
                    fdto.name = mf.Finishing.name;
                    mat_dto.finishes.Add(fdto);
                }

                dto.materials.Add(mat_dto);
            }

            foreach (Dimension dimension in product.dimensions)
            {
                DimensionDTO dim_dto = new DimensionDTO();
                dim_dto.Depth = new MeasureDTO();
                dim_dto.Height = new MeasureDTO();
                dim_dto.Width = new MeasureDTO();
                dim_dto.DimensionId = dimension.DimensionId;
                dim_dto.Depth.Id = dimension.Depth.MeasureId;
                dim_dto.Depth.Value = dimension.Depth.Value;
                dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                dim_dto.Height.Id = dimension.Height.MeasureId;
                dim_dto.Height.Value = dimension.Height.Value;
                dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                dim_dto.Width.Id = dimension.Width.MeasureId;
                dim_dto.Width.Value = dimension.Width.Value;
                dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                dto.dimensions.Add(dim_dto);
            }

            return Ok(dto);
        }

        // PUT: api/Product/id/Dimension/idd
        [HttpPut("{id}/Dimension/{idd}")]
        public async Task<IActionResult> PutProductDimension([FromRoute] int id, [FromRoute] int idd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productRepository.AddDimension(id, idd);

            if (product == null)
            {
                return BadRequest();
            }

            ProductDTO dto = new ProductDTO();
            CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
            dto.ProductId = product.ProductId;
            dto.name = product.name;
            dto.description = product.description;
            dto.dimensions = new List<DimensionDTO>();
            dto.materials = new List<MaterialDTO>();
            foreach (ProductMaterial pm in product.ProductMaterials)
            {
                MaterialDTO mat_dto = new MaterialDTO();
                mat_dto.name = pm.Material.name;
                mat_dto.description = pm.Material.description;
                mat_dto.MaterialId = pm.Material.MaterialId;
                mat_dto.finishes = new List<FinishingDTO>();

                foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                {
                    FinishingDTO fdto = new FinishingDTO();
                    fdto.finishingId = mf.Finishing.FinishingId;
                    fdto.description = mf.Finishing.description;
                    fdto.name = mf.Finishing.name;
                    mat_dto.finishes.Add(fdto);
                }

                dto.materials.Add(mat_dto);
            }

            foreach (Dimension dimension in product.dimensions)
            {
                DimensionDTO dim_dto = new DimensionDTO();
                dim_dto.Depth = new MeasureDTO();
                dim_dto.Height = new MeasureDTO();
                dim_dto.Width = new MeasureDTO();
                dim_dto.DimensionId = dimension.DimensionId;
                dim_dto.Depth.Id = dimension.Depth.MeasureId;
                dim_dto.Depth.Value = dimension.Depth.Value;
                dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                dim_dto.Height.Id = dimension.Height.MeasureId;
                dim_dto.Height.Value = dimension.Height.Value;
                dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                dim_dto.Width.Id = dimension.Width.MeasureId;
                dim_dto.Width.Value = dimension.Width.Value;
                dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                dto.dimensions.Add(dim_dto);
            }

            return Ok(dto);
        }

        // DELETE: api/Product/id/Material/idm
        [HttpDelete("{id}/Material/{idm}")]
        public async Task<IActionResult> DeleteProductMaterial([FromRoute] int id, [FromRoute] int idm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productRepository.RemoveProductMaterial(id, idm);

            if (product == null)
            {
                return BadRequest();
            }

            ProductDTO dto = new ProductDTO();
            CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
            dto.ProductId = product.ProductId;
            dto.name = product.name;
            dto.description = product.description;
            dto.dimensions = new List<DimensionDTO>();
            dto.materials = new List<MaterialDTO>();
            foreach (ProductMaterial pm in product.ProductMaterials)
            {
                MaterialDTO mat_dto = new MaterialDTO();
                mat_dto.name = pm.Material.name;
                mat_dto.description = pm.Material.description;
                mat_dto.MaterialId = pm.Material.MaterialId;
                mat_dto.finishes = new List<FinishingDTO>();

                foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                {
                    FinishingDTO fdto = new FinishingDTO();
                    fdto.finishingId = mf.Finishing.FinishingId;
                    fdto.description = mf.Finishing.description;
                    fdto.name = mf.Finishing.name;
                    mat_dto.finishes.Add(fdto);
                }

                dto.materials.Add(mat_dto);
            }

            foreach (Dimension dimension in product.dimensions)
            {
                DimensionDTO dim_dto = new DimensionDTO();
                dim_dto.Depth = new MeasureDTO();
                dim_dto.Height = new MeasureDTO();
                dim_dto.Width = new MeasureDTO();
                dim_dto.DimensionId = dimension.DimensionId;
                dim_dto.Depth.Id = dimension.Depth.MeasureId;
                dim_dto.Depth.Value = dimension.Depth.Value;
                dim_dto.Depth.ValueMax = dimension.Depth.ValueMax;
                dim_dto.Depth.isDiscrete = dimension.Depth.isDiscrete;
                dim_dto.Height.Id = dimension.Height.MeasureId;
                dim_dto.Height.Value = dimension.Height.Value;
                dim_dto.Height.ValueMax = dimension.Height.ValueMax;
                dim_dto.Height.isDiscrete = dimension.Height.isDiscrete;
                dim_dto.Width.Id = dimension.Width.MeasureId;
                dim_dto.Width.Value = dimension.Width.Value;
                dim_dto.Width.ValueMax = dimension.Width.ValueMax;
                dim_dto.Width.isDiscrete = dimension.Width.isDiscrete;

                dto.dimensions.Add(dim_dto);
            }

            return Ok(dto);
        }
    }
}