using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.UnitTest.Builders
{
    public class PatientModelBuilder
    {
        public new PatientSignUpModel Build()
        {
            return new PatientSignUpModel
            {
                Name = "Test",
                Login = "Test",
                Birthday = DateOnly.Parse("2010-08-02"),
                Password = "Tests"
            };
        }
    }
}
