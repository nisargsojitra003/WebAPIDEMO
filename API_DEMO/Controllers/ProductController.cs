using API_DEMO_BAL.InterFaces;
using API_DEMO_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using API_DEMO_DAL.Models.ProductDTO;

namespace API_DEMO.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _ProductService;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger , IProductService productService,IMapper mapper)
        {
            _logger = logger;
            _ProductService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Products List
        /// </summary>
        /// <returns>Returns all products list.</returns>
        [HttpGet("getlist",Name = "GetProductsList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("api/product/getlist")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            List<Product> productList = await _ProductService.ProductList();
            return Ok(productList);
        }

        /// <summary>
        /// Get product by Product's Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}",Name ="GetProductbyID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Route("api/product/get")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (!await _ProductService.CheckProduct(id))
            {
                return BadRequest();
            }
            Product product = await _ProductService.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("create", Name = "Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Route("api/product/create")]
        public async Task<ActionResult> CreateProduct(CreateProduct product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }
            if (product.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid product ID.");
            }

            bool isCreated = await _ProductService.CreateProductAsync(product);
            if (isCreated)
            {
                return Ok("Product created successfully.");
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create product.");
        }

        /// <summary>
        /// SoftDelete Product by product's Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete/{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Route("api/product/delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool product = await _ProductService.DeleteProduct(id);
            return product == null ? NotFound() : NoContent();
        }

        /// <summary>
        /// Update a Producy by product's id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("update/{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id ,Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }
            var updateProduct =await _ProductService.UpdateProduct(id, product);
            return Ok(updateProduct);
        }

        /// <summary>
        /// Delete Product from Db by Product's id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteFromDb/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            await _ProductService.DeleteProductFromDb(id);
            return Ok();
        }

        /// <summary>
        /// use jsonpatch to update specific property
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchProduct"></param>
        /// <returns></returns>
        [HttpPatch("update/{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductProperty(int id, JsonPatchDocument<Product> patchProduct)
        {
            if(patchProduct == null || id == 0)
            {
                return BadRequest();
            }
            await _ProductService.PartialUpdateProduct(id,patchProduct);
            return NoContent();
        }
    }
}