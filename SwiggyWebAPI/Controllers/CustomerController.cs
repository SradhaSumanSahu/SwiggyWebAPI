using Microsoft.AspNetCore.Mvc;
using SwiggyWebAPI.Data;
using SwiggyWebAPI.Models.Customer;

namespace SwiggyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController : Controller
    {
        private readonly SwiggyAPIDbContext dbcontext;

        public CustomerController(SwiggyAPIDbContext dbcontext)
        {

            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            return Ok(dbcontext.CustomerModel.ToList());

        }
        [HttpGet]
        [Route("{CustomerId:guid}")]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid CustomerId)
        {
            var CustomerModel = await dbcontext.CustomerModel.FindAsync(CustomerId);
            if (CustomerModel == null)
            {
                return NotFound();
            }
            return Ok(CustomerModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddCustomerModel addCustomerModel)
        {
            var CustomerModel = new CustomerModel()
            {
                Id = Guid.NewGuid(),

                CustomerName = addCustomerModel.CustomerName,
                Phone = addCustomerModel.Phone,
                Email = addCustomerModel.Email,
                Address = addCustomerModel.Address,
            };
            await dbcontext.CustomerModel.AddAsync(CustomerModel);
            dbcontext.SaveChangesAsync();
            return Ok(CustomerModel);
        }
        [HttpPut]
        [Route("{CustomerId:guid}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid CustomerId, UpdateCustomerModel updateCustomerModel)
        {
            var CustomerRequestModel = await dbcontext.CustomerModel.FindAsync(CustomerId);
            if (CustomerRequestModel != null)

            {
                CustomerRequestModel.CustomerName = updateCustomerModel.CustomerName;
                CustomerRequestModel.Phone = updateCustomerModel.Phone;
                CustomerRequestModel.Email = updateCustomerModel.Email;
                CustomerRequestModel.Address = updateCustomerModel.Address;

                await dbcontext.SaveChangesAsync();
                return Ok(CustomerRequestModel);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{CustomerId:guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid CustomerId)
        {

            var CustomerModel = await dbcontext.CustomerModel.FindAsync(CustomerId);
            if (CustomerModel != null)
            {
                dbcontext.CustomerModel.Remove(CustomerModel);
                await dbcontext.SaveChangesAsync();
                return Ok(CustomerModel);
            }
            return NotFound();
        }
    }
}