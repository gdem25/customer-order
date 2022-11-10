using CustomerOrderModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private static readonly string[] CustomerNames = new[]
{
            "James Gordon","Hank Thompson", "Lois Lane","Bruce Banner", "John Doe"
        };

        private static readonly string[] OrderNames = new[]
        {
            "Televison", "Tomatoes", "Oven", "Hard Drive", "Pickles", "Raw chicken", "Lettuce",
            "Ice Cream", "Frozen fries", "Sweet Potatoes", "Nutella", "Soda","Orange juice","cucumber",
            "Olive oil", "Soy milk", "Noodles", "Mac and cheese","Frozen burgers", "Frozen Pizza",
            "Televison", "Tomatoes", "Oven", "Hard Drive", "Pickles"
        };
        private static readonly double[] OrderPrices = new[]
        {
           2000.99, 4.53, 375.27, 120.00, 3.68, 7.59, 2.39, 1.79, 11.35, 4.85, 5.43, 2.29, 1.39, 2.93,
           3.59, 3.36, 8.39, 9.62, 12.88, 10.79,2000.99, 4.53, 375.27, 120.00, 3.68
        };


        private readonly CustomerOrdersContext _context;
        public SeedController(CustomerOrdersContext context)
        {
            _context = context;
        }


        [HttpPost("Customers")]
        public async Task<ActionResult> PostCustomers()
        {
            int count = 0;
            for (int i = 0; i < CustomerNames.Length; i++)
            {
                string[] name = CustomerNames[i].Split();
                Customer customer = new()
                {
                    FirstName = name[0],
                    LastName = name[1]
                };
                _context.Customers.Add(customer);
                count++;
            }
            await _context.SaveChangesAsync();
            return new JsonResult(count);
        }

        [HttpPost("Orders")]
        public async Task<ActionResult<int>> PostOrders()
        {
            int count = 0;
            int id = 0;
            if (OrderNames.Length == OrderPrices.Length)
            {
                for (int i = 0; i <OrderNames.Length; i++)
                {
                    if (i % 5 == 0) id++;
                    Order order = new()
                    {
                        OrderName = OrderNames[i],
                        Price = (decimal)OrderPrices[i],
                        CustomerId = id
                    };
                    _context.Orders.Add(order);
                    count++;
                }
                await _context.SaveChangesAsync();
            }
            return new JsonResult(count);
        }
    }
}
