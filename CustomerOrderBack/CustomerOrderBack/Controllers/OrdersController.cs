using CustomerOrderModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrderBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CustomerOrdersContext _context;
        public OrdersController(CustomerOrdersContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("Order/{id:int}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var orderDto = await _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new {
                    o.Id,
                    o.OrderName,
                    o.Price,
                    o.CustomerId
                })
                .SingleOrDefaultAsync();

            return orderDto != null ? Ok(orderDto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new {id = order.Id}, order);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> EditOrder(int id, Order order)
        {
            if(id != order.Id)
            {
                return BadRequest();
            }
            _context.Entry(order).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        private bool OrderExists(int id) => _context.Orders.Any(o => o.Id == id);
    }
}
