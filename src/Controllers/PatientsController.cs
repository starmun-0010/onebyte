using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneByte.Contracts.RequestModels.Patient;
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
    public class PatientsController : OneByteControllerBase
    {
        public PatientsController(OneByteDbContext context, IMapper mapper) : base(context, mapper)
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
            return Ok(_mapper.Map<PatientResponseModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Patients
            .Select(patient =>_mapper.Map<PatientResponseModel>(patient))
            .ToListAsync());
        }

        [HttpGet]
        [Route("{id}/doctors")]
        public async Task<IActionResult> GetDoctors(Guid id)
        {
            var result = await _context.Patients.FindAsync(id);
            if(result == null)
            {
                throw new ResourceNotFoundException(nameof(Patient), id);
            }
            var doctors = (await _context.Patients.Include(d=>d.Doctors).FirstAsync(p=> p.ID == id)).Doctors;
            return Ok(_mapper.Map<List<DoctorResponseModel>>(doctors));
        }

        [HttpPost]
        public async Task<IActionResult> Post(PatientPostRequestModel patientPostRequestModel)
        {
            var patient = _mapper.Map<Patient>(patientPostRequestModel);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = patient.ID }, _mapper.Map<PatientResponseModel>(patient));
        }

        [HttpPut]
        public async Task<IActionResult> Put(PatientPutRequestModel patient)
        {
            _context.Patients.Update(_mapper.Map<Patient>(patient));
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<PatientResponseModel>(await _context.Patients.FindAsync(patient.ID)));
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