using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Interfaces;
using Backend.Dto;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PaginationDto<Product>))]
        public IActionResult GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var products = _productsRepository.GetProducts(pageNumber, pageSize);

            if(!ModelState.IsValid) {
                return BadRequest();
            }

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if(!_productsRepository.ProductExists(productId)){
                return NotFound();
            }

            if(!ModelState.IsValid) {
                return BadRequest();
            }

            var product = _productsRepository.GetProduct(productId);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromBody] Product productCreated)
        {
            if(productCreated == null) {
                return BadRequest(ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _productsRepository.CreateProduct(productCreated);

            return Ok(result);
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(int productId, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(ModelState);
            }

            if (productId != product.Id)
            {
                return BadRequest();
            }

            if (!_productsRepository.ProductExists(productId))
            {
                return NotFound();
            }

            if (!_productsRepository.UpdateProduct(product))
            {
                return StatusCode(500, "Something went wrong while updating the product");
            }

            return NoContent();
        }


    }
}