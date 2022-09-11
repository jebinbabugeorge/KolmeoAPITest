using KolmeoAPI.BLayer;
using KolmeoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KolmeoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IBusinessLayer _businessLayer;

        public ProductController(IBusinessLayer businessLayer)
        {
            _businessLayer = businessLayer;
        }

        /// <summary>
        /// Returns all the products
        /// </summary>
        /// <returns>a list of products</returns>
        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _businessLayer.GetProductsAsync();

            return Ok(products);
        }

        /// <summary>
        /// An api to get a single product
        /// </summary>
        /// <param name="Id">A non-zero integer unique identifier for each product.</param>
        /// <returns>A product</returns>
        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int Id)
        {
            var product = await _businessLayer.GetProductAsync(Id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// An api that will create a valid product in the database
        /// </summary>
        /// <param name="productDto">Details of a product</param>
        /// <returns>The product that was created</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO productDto)
        {
            productDto.Id = await _businessLayer.CreateProductAsync(productDto);

            return Ok(productDto);
        }
    }
}
