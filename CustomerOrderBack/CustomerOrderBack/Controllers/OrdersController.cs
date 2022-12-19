using CustomerOrderBack.Dtos;
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
        public async Task<ActionResult<Order>> AddOrder(OrderDto neworder)
        {
            Order order = new()
            {
                OrderName = neworder.OrderName,
                Price = (decimal)neworder.Price,
                CustomerId = neworder.CustomerId
            };
            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                throw;
            }
            
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> EditOrder(int id, OrderDto order)
        {
            Order newOrder = new()
            {
                Id= order.Id,
                OrderName = order.OrderName,
                Price = (decimal)order.Price,
                CustomerId = order.CustomerId
            };

            if (id != order.Id)
            {
                return BadRequest();
            }
            _context.Entry(newOrder).State = EntityState.Modified;
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteOrder(int id) {
            Order? order = await _context.Orders.FindAsync(id);
            if(order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool OrderExists(int id) => _context.Orders.Any(o => o.Id == id);
    }
}
