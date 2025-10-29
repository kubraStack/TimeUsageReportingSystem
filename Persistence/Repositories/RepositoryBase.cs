using Application.Models.Paging;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>().AsQueryable();
            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }
            return await query 
                .Where(predicate)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<ListModel<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int index = 0, int size = 10, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            //temel Iqueryable oluştur
            IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

            // Quey filterlarını yoksay
            if (ignoreQueryFilters) {
                query = query.IgnoreQueryFilters();
            }

            //filtreleme uygula
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            //toplam kayıt sayısı hesaplama
            int count = await query.CountAsync(cancellationToken);

            //sıralama uygula
            if (orderBy != null)
            {
                //gelen sıralama fonksiyonunu uygula
                query = orderBy(query);
            }
            else
            {
                // eğer orderBy yoksa varsayılan sıralama (Id'ye göre) uygula
                query = query.OrderBy(e => EF.Property<int>(e, "Id"));
            }

            //sayfalama uygula
            query = query.Skip(index * size).Take(size); //İstebilen sayfaya atla ve sayfa boyutu kadar kayıt getir.

            //Veriyi çek
            List<T> items = await query.ToListAsync(cancellationToken);
            return new ListModel<T> { 
                Items = items,
                Index = index,
                Size = size,
                Count = count,
                Pages = (int)Math.Ceiling((double)count / size)
            };

        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync(); //Veri okunabilirliği için sıralama
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
