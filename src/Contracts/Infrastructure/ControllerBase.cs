using Microsoft.AspNetCore.Mvc;
using OneByte.Data;

namespace OneByte.Infrastructure
{
    public class OneByteControllerBase : Controller
    {
        protected readonly OneByteDbContext _context;
        public OneByteControllerBase(OneByteDbContext context)
        {
            _context = context;
        }
    }
}