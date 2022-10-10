using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderBack.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CustomerOrderController : ControllerBase
    {
        private static readonly string[] CustomerNames = new[] 
        { 
            "James Gordon","Hank Tompson", "Lois Lane","Bruce Banner", "John Doe" 
        };

        private static readonly string[] OrderNames = new[]
        {
            "Televison", "Tomatoes", "Oven", "Hard Drive", "Pickles", "Raw chicken", "Lettuce",
            "Ice Cream", "Frozen fries", "Sweet Potatoes", "Nutella", "Soda","Orange juice"
        };

        private readonly ILogger<CustomerOrderController> _logger;

        public CustomerOrderController(ILogger<CustomerOrderController> logger)
        {
            _logger = logger;
        }
        private static Customer[] CustomerArray()
        {
            Customer[] result = new Customer[5];
            for(int i=0;i<result.Length;i++) { 
                result[i] = new Customer { Id= i+1, FullName= CustomerNames[i] };
            }
            return result;
        }
        private static readonly Customer[] Customers = CustomerArray();
        private static IEnumerable<Order> OrdersArray() {
            return Enumerable.Range(0, 13).Select(index => new Order 
            { 
                Id = (index+1),
                OrderName = OrderNames[index],
                CustomerId = Customers[Random.Shared.Next(CustomerNames.Length)].Id
            }).ToArray();
        }

        [HttpGet]
        public Customer[] GetCustomers()
        {
            return Customers;
        }
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            return OrdersArray();
        }
    }
}
