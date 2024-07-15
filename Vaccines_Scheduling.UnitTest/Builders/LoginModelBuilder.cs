using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.Model;

namespace Vaccines_Scheduling.UnitTest.Builders
{
    public class LoginModelBuilder
    {
        public new PatientLoginModel Build()
        {
            return new PatientLoginModel
            {
                Login = "Test",
                Password = "12345"
            };
        }
    }
}
