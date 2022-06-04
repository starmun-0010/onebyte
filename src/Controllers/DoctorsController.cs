using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneByte.Contracts.ResponseModels;
using OneByte.Data;
using OneByte.DomainModels;
using OneByte.Infrastructure;
using OneByte.Infrastructure.Exceptions;

namespace OneByte.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : OneByteControllerBase
    {
        public DoctorsController(OneByteDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {   var result = await _context.Doctors.FindAsync(id);
            if(result == null)
            {
                throw new ResourceNotFoundException(nameof(Doctor), id);
            }
            
            return Ok(_mapper.Map<Doctor, DoctorResponseModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Doctors
            .Select(doctor =>_mapper.Map<DoctorResponseModel>(doctor))
            .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = doctor.ID }, _mapper.Map<DoctorResponseModel>(doctor));
        }

        [HttpPut]
        public async Task<IActionResult> Put(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<DoctorResponseModel>(await _context.Doctors.FindAsync(doctor.ID)));
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