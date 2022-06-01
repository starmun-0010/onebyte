using System.Collections.Generic;

namespace OneByte.Models
{
    public class Patient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public List<Visit> Visits { get; set; }
    }
}