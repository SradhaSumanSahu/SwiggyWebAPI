using Microsoft.AspNetCore.Mvc;
using SwiggyWebAPI.Data;
using SwiggyWebAPI.Models.Order;

namespace SwiggyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderController : Controller
    {
        private readonly SwiggyAPIDbContext dbcontext;

        public OrderController(SwiggyAPIDbContext dbcontext)
        {

            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            return Ok(dbcontext.OrderModel.ToList());

        }
        [HttpGet]
        [Route("{OrderId:int}")]
        public async Task<IActionResult> GetOrder([FromRoute] int OrderId)
        {
            var OrderModel = await dbcontext.OrderModel.FindAsync(OrderId);
            if (OrderModel == null)
            {
                return NotFound();
            }
            return Ok(OrderModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderModel addOrderModel)
        {
            var OrderModel = new OrderModel()
            {
                

                CustomerName = addOrderModel.CustomerName,
                Location = addOrderModel.Location,
                Email = addOrderModel.Email,
            };
            await dbcontext.OrderModel.AddAsync(OrderModel);
            dbcontext.SaveChangesAsync();
            return Ok(OrderModel);
        }
        [HttpPut]
        [Route("{OrderId:int}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int OrderId, UpdateOrderModel updateOrderModel)
        {
            var OrderRequestModel = await dbcontext.OrderModel.FindAsync(OrderId);
            if (OrderRequestModel != null)

            {
                OrderRequestModel.CustomerName = updateOrderModel.CustomerName;
                OrderRequestModel.Location = updateOrderModel.Location;
                OrderRequestModel.Email = updateOrderModel.Email;

                await dbcontext.SaveChangesAsync();
                return Ok(OrderRequestModel);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{OrderId:int}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute]int  OrderId)
        {

            var OrderModel = await dbcontext.OrderModel.FindAsync(OrderId);
            if (OrderModel != null)
            {
                dbcontext.OrderModel.Remove(OrderModel);
                await dbcontext.SaveChangesAsync();
                return Ok(OrderModel);
            }
            return NotFound();
        }
    }
}