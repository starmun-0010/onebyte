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
    public class DoctorsController : Controller
    {
        private OneByteDbContext _context;
        public DoctorsController(OneByteDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _context.Doctors.FindAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Doctors.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = doctor.ID }, doctor);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return Ok(await _context.Doctors.FindAsync(doctor.ID));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            _context.Doctors.Remove(await _context.Doctors.FindAsync(id));
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}