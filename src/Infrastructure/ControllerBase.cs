using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneByte.Data;

namespace OneByte.Infrastructure
{
    public class OneByteControllerBase : Controller
    {
        protected readonly OneByteDbContext _context;
        protected readonly IMapper _mapper;

        public OneByteControllerBase(OneByteDbContext context, IMapper mapper=null)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}