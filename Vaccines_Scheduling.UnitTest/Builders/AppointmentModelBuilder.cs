using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.UnitTest.Builders
{
    public class AppointmentModelBuilder
    {
        public new AppointmentSignUpModel Build()
        {
            return new AppointmentSignUpModel
            {
                PatientName = "Test",
                Birthday = DateOnly.Parse("2010-08-02"),
                Date = DateOnly.Parse("2024-08-14"),
                Time = TimeOnly.Parse("18:00")
            };
        }
    }
}
