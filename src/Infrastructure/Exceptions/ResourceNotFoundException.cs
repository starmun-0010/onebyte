using System;

namespace OneByte.Infrastructure.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string resourceName, Guid id) : base($"{resourceName} with id {id} not found.")
        {
        }
    }
}