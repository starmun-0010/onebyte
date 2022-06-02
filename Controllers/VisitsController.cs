using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneByte.Data;
using OneByte.Models;

namespace OneByte.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VisitsController: Controller
    {
        private OneByteDbContext _context;
        public VisitsController(OneByteDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _context.Visits.FindAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Visits.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Visit visit)
        {
            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = visit.ID }, visit);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Visit visit)
        {
            _context.Visits.Update(visit);
            await _context.SaveChangesAsync();
            return Ok(await _context.Visits.FindAsync(visit.ID));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            _context.Visits.Remove(await _context.Visits.FindAsync(id));
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}