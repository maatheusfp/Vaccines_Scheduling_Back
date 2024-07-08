using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccines_Scheduling.Entity.Entities
{
    public class Patient : IdEntity<int>
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public DateTime Birthday { get; set; }

        public DateTime CreationTime { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // precisa criar a lista de agendamentos ?
        public Patient() { }
    }
}
