using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Entity.Entities;
using Vaccines_Scheduling.Repository.Interface.IRepositories;

namespace Vaccines_Scheduling.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity 
    {
        protected readonly Context _context;
        protected virtual DbSet<TEntity> EntitySet { get;}

        public BaseRepository(Context context)
        {
            _context = context;
            EntitySet = _context.Set<TEntity>();
        }

        public async Task Insert(TEntity entity)
        {
            await EntitySet.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            EntitySet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await EntitySet.FindAsync(id);
        }
        public async Task Update(TEntity entity)
        {
            EntitySet.Update(entity);

            await _context.SaveChangesAsync();

        }
    }
}
