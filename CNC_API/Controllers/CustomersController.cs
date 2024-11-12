using CNC_shared.Data;
using CNC_shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CNC_API.Controllers
{

    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var costumers = await _context.customers.ToListAsync();

            return Ok(costumers);
        }

        [HttpGet("{CI}")]
        public async Task<ActionResult> GetCIAsync(string CI)
        {
            var costumer = await _context.customers.FirstOrDefaultAsync(x => x.CI == CI);

            try
            {
                return Ok(costumer);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> PostCostumer(Customers customers)
        {
            try
            {
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return Ok(customers);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplica"))
                {
                    return BadRequest("Ya existe un cliente con la misma CI");
                }
                return BadRequest(dbUpdateException.InnerException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
