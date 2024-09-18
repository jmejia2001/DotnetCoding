using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _productService.GetAllProducts();
            if(productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }
        [HttpGet("approvalQueue")]
        public async Task<IActionResult> GetApprovalQueue()
        {
            var approvalQueue = await _productService.GetApprovalQueue();
            return Ok(approvalQueue);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDetails product)
        {
            try
            {
                await _productService.Create(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDetails product)
        {
            try
            {
                product.Id = id;
                await _productService.Update(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
