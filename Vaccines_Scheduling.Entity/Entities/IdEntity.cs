using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccines_Scheduling.Entity.Entities
{
    public abstract class IdEntidade<T> : IEntidade
    {
        public T Id { get; set; }
    }
}
