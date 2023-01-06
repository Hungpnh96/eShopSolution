using eShopSolution.Application.Catalog.Products;
using eShopSolution.ViewModels.Catalog.ProductImage;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Service
        private readonly IProductService _ProductService;

        public ProductsController(  IProductService manageProductService)
        {
            _ProductService = manageProductService;
        }

        //http://localhost:port/product/public-paging
        [HttpGet("{languageId}")]
        [Authorize]
        public async Task<IActionResult> GetAllPagging(string languageId , [FromQuery]GetPublicProductPagingRequest reuqest)
        {
            var products = await _ProductService.GetAllByCategoryId(languageId,reuqest);
            return Ok(products); // view statuscode in restfull API
        }

        //http://localhost:port/product/1
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _ProductService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product); //Status: 200
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }    
            var productId = await _ProductService.Create(request);
            if (productId == 0)
                return BadRequest(); // Status : 400

            var product = await _ProductService.GetById(productId, request.LanguageId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _ProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest(); // Status : 400
            return Ok(); // Status: 200
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _ProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest(); // Status : 400
            return Ok(); // Status: 200
        }

        [HttpPatch("{productId}/{newPrice}")] // update 1 phần dùng httpPatch
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice) 
        {
            var isSuccessful = await _ProductService.UpdatePrice(productId, newPrice);
            if (isSuccessful)
                return Ok();// Status: 200

            return BadRequest();// Status : 400
        }

        //ProductImage
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId , [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _ProductService.AddImages(productId,request);
            if (imageId == 0)
                return BadRequest(); // Status : 400

            var image = await _ProductService.GetImageById(imageId);


            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{productId}/images")]
        public async Task<IActionResult> UpdateImage([FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ProductService.UpdateImage(request);
            if (result == 0)
                return BadRequest(); // Status : 400
            return Ok();// Status : 200
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ProductService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest(); // Status : 400
            return Ok();// Status : 200
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _ProductService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find image");
            return Ok(image); //Status: 200
        }
    }
}
