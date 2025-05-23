﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccines_Scheduling.Entity.Entities
{
    public class Appointment : IdEntity<int>
    {
        public int IdPatient { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }
        public string Status { get; set; }
        public DateTime CreationTime { get; set; }
        public Appointment() { }
    }
}
