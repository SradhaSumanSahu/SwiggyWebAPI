using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwiggyWebAPI.Data;
using SwiggyWebAPI.Models.Product;

namespace SwiggyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    

    public class ProductController : Controller
    {
        private readonly SwiggyAPIDbContext dbcontext;

        public ProductController(SwiggyAPIDbContext dbcontext)
        {

            this.dbcontext = dbcontext;
        }
        [HttpGet,Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProduct()
        {
            return Ok(dbcontext.ProductModel.ToList());

        }
        [HttpGet]
        [Route("{ProductId:guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid ProductId)
        {
            var ProductModel = await dbcontext.ProductModel.FindAsync(ProductId);
            if (ProductModel == null)
            {
                return NotFound();
            }
            return Ok(ProductModel);
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(AddProductModel addProductModel)
        {
            var ProductModel = new ProductModel()
            {
                Id = Guid.NewGuid(),

                ProductName = addProductModel.ProductName,
                Price = addProductModel.Price,
                Type = addProductModel.Type,
            };
            await dbcontext.ProductModel.AddAsync(ProductModel);
            dbcontext.SaveChangesAsync();
            return Ok(ProductModel);
        }
        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{ProductId:guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid ProductId, UpdateProductModel updateProductModel)
        {
            var ProductRequestModel = await dbcontext.ProductModel.FindAsync(ProductId);
            if (ProductRequestModel != null)

            {
                ProductRequestModel.ProductName = updateProductModel.ProductName;
                ProductRequestModel.Price = updateProductModel.Price;
                ProductRequestModel.Type = updateProductModel.Type;

                await dbcontext.SaveChangesAsync();
                return Ok(ProductRequestModel);
            }
            return NotFound();
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{ProductId:guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid ProductId)
        {

            var ProductModel = await dbcontext.ProductModel.FindAsync(ProductId);
            if (ProductModel != null)
            {
                dbcontext.ProductModel.Remove(ProductModel);
                await dbcontext.SaveChangesAsync();
                return Ok(ProductModel);
            }
            return NotFound();
        }
    }
}