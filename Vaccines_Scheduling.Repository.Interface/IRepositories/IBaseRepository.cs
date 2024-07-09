using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.Entities;

namespace Vaccines_Scheduling.Repository.Interface.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task Insert(TEntity entity);
        Task Delete(TEntity entity);
    }
}
