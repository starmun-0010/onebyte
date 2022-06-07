using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneByte.Contracts.RequestModels.Visit;
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
    public class VisitsController: OneByteControllerBase
    {
        public VisitsController(OneByteDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _context.Visits
            .Include(v=>v.Doctor)
            .Include(v=>v.Patient)
            .FirstOrDefaultAsync(v => v.ID == id);
            if(result == null)
            {
                throw new ResourceNotFoundException(nameof(Visit), id);
            }
            return Ok(_mapper.Map<VisitResponseModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Visits
            .Include(v=>v.Doctor)
            .Include(v=>v.Patient)
            .Select(v => _mapper.Map<VisitResponseModel>(v))
            .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(VisitPostRequestModel visitPostRequestModel)
        {
            var visit = _mapper.Map<Visit>(visitPostRequestModel);
            _context.Visits.Add(visit);
            
            var doctor = await _context.Doctors.Include(d=>d.Patients).FirstAsync(d=>d.ID == visit.DoctorId);
            if(doctor == null)
            {
                throw new ResourceNotFoundException(nameof(Doctor), visit.DoctorId);
            }
            var patient = await _context.Patients.FindAsync(visit.PatientId);
            if(patient == null)
            {
                throw new ResourceNotFoundException(nameof(Patient), visit.PatientId);
            }
            doctor.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = visit.ID }, _mapper.Map<VisitResponseModel>(visit));
        }

        [HttpPut]
        public async Task<IActionResult> Put(VisitPutRequestModel visit)
        {
            _context.Visits.Update(_mapper.Map<Visit>(visit));
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<VisitResponseModel>(await _context.Visits.FindAsync(visit.ID)));
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