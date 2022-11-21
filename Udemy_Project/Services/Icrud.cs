using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_Project.Services
{
    internal interface Icrud<TEntity, in Tpk> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> GetAsync(Tpk id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(Tpk id, TEntity entity);

        Task<TEntity> DeleteAsync(Tpk id);
    }
}
