using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccines_Scheduling.Entity.Model
{
    public class PatientSignUpModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
    }
}
