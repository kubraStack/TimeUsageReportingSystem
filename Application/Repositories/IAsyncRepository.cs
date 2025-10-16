using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IAsyncRepository<T> where T : class
    {
        //Okuma işlemleri
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
