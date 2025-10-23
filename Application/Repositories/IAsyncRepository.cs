using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IAsyncRepository<T> where T : class
    {
        //Okuma işlemleri
        Task<T?> GetAsync(
            Expression<Func<T, bool>> predicate,
            bool ignoreQueryFilters = false, // Opsiyonel: Global filtreleri atlamak için
            CancellationToken cancellationToken = default );
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        //Oluşturma işlemleri
        Task<T> AddAsync(T entity);

        //Güncelleme işlemleri
        Task UpdateAsync(T entity);

        //Silme işlemleri
        Task DeleteAsync(T entity);
    }
}
