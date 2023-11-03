using Jake.Assessment.DTO;
using Jake.Assessment.Implementation.Services;
using Jake.Assessment.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Jake.Assessment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        public async Task<IActionResult> AddNewProduct(CreateProductRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid Model");
            }
            var addProduct = await _productService.CreateProduct(model);
            if (!addProduct.Status)
            {
                return BadRequest();
            }
            return Ok(addProduct);
        }
        [HttpPatch]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid Model");
            }
            var updateProduct = await _productService.UpdateProduct(model);
            if (!updateProduct.Status)
            {
                return BadRequest();
            }
            return Ok(updateProduct);
        }
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        public async Task<IActionResult>DeleteProduct([Required] int productId)
        {
            var deleteProduct = await _productService.DeleteProduct(productId);
            if(deleteProduct.Status)
                return Ok(deleteProduct);

            return BadRequest(deleteProduct);
        }
        [HttpPatch]
        [ProducesResponseType(typeof(IEnumerable<BaseResponseModel<IEnumerable<AllProductViewModel>>>), 200)]
        public async Task<IActionResult> AllProduct()
        {
            var products = await _productService.AllProduct();
            if(products.Status)
                return Ok(products);
            return BadRequest(products);
        }
    }
}
