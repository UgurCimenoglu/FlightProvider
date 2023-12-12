using FlightProject.Core.DataAccess;
using FlightProject.Core.Entities;
using FlightProject.DataAccess.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.DataAccess.Concrete
{
    public class EfEntityRepositoryBase<T> : IEntityRepository<T> where T : BaseEntity
    {
        private readonly FlightDbContext _context;

        public EfEntityRepositoryBase(FlightDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking) query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry != null;
        }

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }

        public bool Update(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry != null;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entry = Table.Remove(model);
            return entry != null;
        }

        public async Task<bool> RemoveByIdAsync(string id)
        {
            var data = await Table.FirstOrDefaultAsync(q => q.Id == Guid.Parse(id));
            if (data != null) return Remove(data);
            return false;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
