using Microsoft.AspNetCore.Mvc;
using SiC.DTOs;
using SiC.Models;
using SiC.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiC.Controllers
{
    [Route("api/Collection")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly CollectionRepository collectionRepository;

        public CollectionController(SiCContext context)
        {
            collectionRepository = new CollectionRepository(context);
        }

        // GET: api/Collection
        [HttpGet]
        public IEnumerable<CollectionDTO> GetCollection()
        {
            List<CollectionDTO> dtos = new List<CollectionDTO>();
            foreach (Collection collec in collectionRepository.FindAll())
            {
                CollectionDTO dto = new CollectionDTO();
                dto.CollectionId = collec.CollectionId;
                dto.collectionName = collec.collectionName;
                dto.aestheticParameter = collec.aestheticParameter;
                dtos.Add(dto);
            }

            return dtos;
        }

        // GET: api/Collection/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCollection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collection = await collectionRepository.FindById(id);

            if (collection == null)
            {
                return NotFound();
            }

            CollectionDTO dto = new CollectionDTO();
            dto.CollectionId = collection.CollectionId;
            dto.collectionName = collection.collectionName;
            dto.aestheticParameter = collection.aestheticParameter;

            foreach (Product prod in collection.products)
            {
                ProductDTO prodDTO = new ProductDTO();
                prodDTO.ProductId = prod.ProductId;
                prodDTO.name = prod.name;
                prodDTO.description = prod.description;

                CategoryDTO catDTO = new CategoryDTO();
                catDTO.CategoryId = prod.category.CategoryId;
                catDTO.name = prod.category.name;
                catDTO.description = prod.category.description;

                dto.products.Add(prodDTO);
            }

            return Ok(dto);
        }

        // PUT: api/Collection/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection([FromRoute] int id, [FromBody] CollectionDTO collectionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != collectionDTO.CollectionId)
            {
                return BadRequest();
            }

            var collection = await collectionRepository.FindById(id);

            if (collection == null)
            {
                return BadRequest();
            }

            CollectionDTO dto = new CollectionDTO();
            dto.CollectionId = collection.CollectionId;
            dto.collectionName = collection.collectionName;
            dto.aestheticParameter = collection.aestheticParameter;

            ///TODO Atualizar os produtos, ver o controller de materiais para ver como faz com os finishings

            return Ok(dto);
        }

        // POST: api/Collection
        [HttpPost]
        public async Task<IActionResult> PostCollection([FromBody] CollectionDTO collectionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collection = await collectionRepository.Add(collectionDTO);

            CollectionDTO colDTO = new CollectionDTO();
            colDTO.CollectionId = collection.CollectionId;
            colDTO.collectionName = collection.collectionName;
            colDTO.aestheticParameter = collection.aestheticParameter;

            foreach (Product product in collection.products)
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
                    mat_dto.finishes = new List<FinishingDTO>();

                    foreach (MaterialFinishing mf in pm.Material.MaterialFinishings)
                    {
                        FinishingDTO fdto = new FinishingDTO();
                        fdto.finishingId = mf.Finishing.FinishingId;
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
                collectionDTO.products.Add(dto);
            }

            return CreatedAtAction("PostCollection", colDTO);
        }

        // DELETE: api/Collection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collection = await collectionRepository.Remove(id);
            if (collection == null)
            {
                return NotFound();
            }

            CollectionDTO dto = new CollectionDTO();
            dto.CollectionId = collection.CollectionId;
            dto.collectionName = collection.collectionName;
            dto.aestheticParameter = collection.aestheticParameter;

            foreach (Product prod in collection.products)
            {
                ProductDTO prodDTO = new ProductDTO();
                prodDTO.ProductId = prod.ProductId;
                prodDTO.name = prod.name;
                prodDTO.description = prod.description;

                CategoryDTO catDTO = new CategoryDTO();
                catDTO.CategoryId = prod.category.CategoryId;
                catDTO.name = prod.category.name;
                catDTO.description = prod.category.description;

                dto.products.Add(prodDTO);
            }

            return Ok(dto);
        }
    }
}