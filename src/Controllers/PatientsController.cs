using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneByte.Data;
using OneByte.DomainModels;
using OneByte.Infrastructure;
using OneByte.Infrastructure.Exceptions;

namespace OneByte.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : OneByteControllerBase
    {
        public PatientsController(OneByteDbContext context) : base(context)
        {
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {   
            var result = await _context.Patients.FindAsync(id);
            if(result == null)
            {
                throw new ResourceNotFoundException(nameof(Patient), id);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Patients.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = patient.ID }, patient);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return Ok(await _context.Patients.FindAsync(patient.ID));
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            _context.Patients.Remove(await _context.Patients.FindAsync(id));
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}