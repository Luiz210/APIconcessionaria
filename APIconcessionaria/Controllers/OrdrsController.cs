using APIconcessionaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static APIconcessionaria.Context.AppdbContext;

namespace APIconcessionaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OredersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OredersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(Orders orders)
        {
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrders), new { id = orders.Id }, orders);
        }

    }
}
