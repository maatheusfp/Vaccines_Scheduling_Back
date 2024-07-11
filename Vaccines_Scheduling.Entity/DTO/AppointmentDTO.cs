using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccines_Scheduling.Entity.DTO
{
    public class AppointmentDTO
    {
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }
        public string Status { get; set; }
    }
}
