using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public interface IGenericRepository<T> where T : class
        //repositorio de T = para qualquer entidade (classe)
    {
        IQueryable<T> GetAll(); //tabela de T

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);
    }
}
