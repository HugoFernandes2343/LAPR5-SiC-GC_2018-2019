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
    [Route("api/Catalog")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogRepository catalogRepository;

        public CatalogController(SiCContext context)
        {
            catalogRepository = new CatalogRepository(context);
        }

        // GET: api/Catalog
        //Returns all the catalogs
        [HttpGet]
        public IEnumerable<CatalogDTO> GetCatalog()
        {
            List<CatalogDTO> dtos = new List<CatalogDTO>();
            foreach (Catalog catalog in catalogRepository.FindAll())
            {
                CatalogDTO dto = new CatalogDTO();
                dto.CatalogId = catalog.CatalogId;
                dto.Date = catalog.Date;
                dto.CatalogDescription = catalog.CatalogDescription;
                dto.CatalogName = catalog.CatalogName;
                dtos.Add(dto);
            }

            return dtos;
        }

        // GET: api/Catalog/id
        //Returns a specific catalog
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await catalogRepository.FindById(id);

            if (catalog == null)
            {
                return NotFound();
            }

            CatalogDTO dto = new CatalogDTO();
            dto.CatalogId = catalog.CatalogId;
            dto.Date = catalog.Date;
            dto.CatalogDescription = catalog.CatalogDescription;

            foreach (Product prod in catalog.Products)
            {
                ProductDTO prodDTO = new ProductDTO();
                prodDTO.ProductId = prod.ProductId;
                prodDTO.name = prod.name;
                prodDTO.description = prod.description;
                //get Category
                CategoryDTO catDTO = new CategoryDTO();
                catDTO.CategoryId = prod.category.CategoryId;
                catDTO.name = prod.category.name;
                catDTO.description = prod.category.description;

                dto.products.Add(prodDTO);
            }

            return Ok(dto);
        }

        // PUT: api/Catalog/id
        //Updates a catalog with the given id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalog([FromRoute] int id, [FromBody] CatalogDTO catalogDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != catalogDTO.CatalogId)
            {
                return BadRequest();
            }

            var catalog = await catalogRepository.FindById(id);

            if (catalog == null)
            {
                return BadRequest();
            }

            CatalogDTO dto = new CatalogDTO();
            dto.CatalogId = catalog.CatalogId;
            dto.CatalogDescription = catalog.CatalogDescription;
            dto.Date = catalog.Date;

            return Ok(dto);
        }

        // POST: api/Catalog
        // Creates a catalog
        [HttpPost]
        public async Task<IActionResult> PostCatalog([FromBody] CatalogDTO catalogDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await catalogRepository.Add(catalogDTO);

            CatalogDTO catalogDTO = new CatalogDTO();
            catalogDTO.CatalogId = catalog.CatalogId;
            catalogDTO.CatalogDescription = catalog.CatalogDescription;
            catalogDTO.Date = catalog.Date;

            foreach (ProductDTO dto in catalog.products)
            {
                ProductDTO dto = new ProductDTO();
                CategoryDTO cat_dto = new CategoryDTO(product.category.name, product.category);
                dto.ProductId = product.ProductId;
                dto.name = product.name;
                dto.dimensions = new List<DimensionDTO>();
                dto.materials = new List<MaterialDTO>();
                foreach (ProductMaterial pm in product.ProductMaterials)
                {
                    MaterialDTO mat_dto = new MaterialDTO();
                    mat_dto.name = pm.Material.name;
                    mat_dto.MaterialId = pm.Material.MaterialId;
                    mat_dto.finishesDTO = new List<FinishingDTO>();

                    foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                    {
                        FinishingDTO fdto = new FinishingDTO();
                        fdto.finishingId = mf.Finishing.FinishingId;
                        fdto.name = mf.Finishing.name;
                        mat_dto.finishesDTO.Add(fdto);
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
                catalogDTO.products.Add(dto);
            }

            return CreatedAtAction("PostCatalog", catalogDTO);
        }

        // DELETE: api/Catalog/id
        // Deletes a catalog with the given id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = await catalogRepository.Remove(id);
            if (catalog == null)
            {
                return NotFound();
            }

            CatalogDTO dto = new CatalogDTO();
            dto.CatalogId = catalog.CatalogId;
            dto.Date = catalog.Date;
            dto.CatalogDescription = catalog.CatalogDescription;

            return Ok(dto);
        }
    }
}