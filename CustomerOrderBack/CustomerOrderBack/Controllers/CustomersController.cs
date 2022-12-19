using CustomerOrderBack.Dtos;
using CustomerOrderModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace CustomerOrderBack.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerOrdersContext _context;
        public CustomersController(CustomerOrdersContext context)
        {
            _context = context;
        }
        // GET: CustomersController

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerOrder>> GetCustomerOrders(int id)
        {
            CustomerOrder? customerDto = await _context.Customers
                .Where(x => x.Id == id)
                .Select(x => new CustomerOrder
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Orders = x.Orders
                }).SingleOrDefaultAsync();
            if (customerDto == null) return NotFound();
            return Ok(customerDto);
        }

        [HttpGet("Total/{id:int}")]
        public async Task<ActionResult<OrderTotal>> GetCustomerOrderTotal(int id) {
            OrderTotal? totalDto = await _context.Customers
                .Where(x => x.Id == id)
                .Select(x => new OrderTotal
                {
                    Id = x.Id,
                    Total = x.Orders.Select(t => t.Price).Sum()
                }).SingleOrDefaultAsync();
            if (totalDto == null) return NotFound();
            return Ok(totalDto);
        }

        [HttpGet("Customer/{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customerDto = await _context.Customers
                .Where(x => x.Id == id)
                .Select(c => new {c.Id, c.FirstName, c.LastName })
                .SingleOrDefaultAsync();
            return customerDto != null ? Ok(customerDto) : NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> EditCustomer(int id, Customer customer)
        {
            if(id != customer.Id)
            {
                return BadRequest();
            }
            _context.Entry(customer).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            Customer? customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool CustomerExists(int id) => _context.Customers.Any(c => c.Id == id);

    }
}
