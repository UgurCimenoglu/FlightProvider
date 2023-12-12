using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightProject.Core.DataAccess
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Update(T model);
        bool Remove(T model);
        Task<bool> RemoveByIdAsync(string id);
        Task<int> SaveAsync();
    }
}
