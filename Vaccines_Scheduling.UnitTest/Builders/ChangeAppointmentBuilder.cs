using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.UnitTest.Builders
{
    public class ChangeAppointmentBuilder
    {
        public new AppointmentChangeModel Build()
        {
            return new AppointmentChangeModel
            {
                Date = DateOnly.Parse("2024-08-14"),
                Time = TimeOnly.Parse("18:00"),
                Status = "Test"
            };
        }
    }
}
