using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentAPI.Domain
{
    public class Holiday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
