using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentAPI.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TokenNumber { get; set; }
    }
}