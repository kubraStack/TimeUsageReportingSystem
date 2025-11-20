using Core.Application.Pipelines.Chaching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task AddAsync(string key, object value, int duration) => Task.Run(() => Add(key, value, duration));
        public void Add(string key, object value, int duration) => _memoryCache.Set(key, value, TimeSpan.FromSeconds(duration));

        public Task<T> GetAsync<T>(string key) => Task.FromResult((T)_memoryCache.Get(key));
        public object Get(string key) => _memoryCache.Get(key);

        public bool IsAdd(string key) => _memoryCache.TryGetValue(key, out _);

        public Task RemoveAsync(string key) => Task.Run(() => _memoryCache.Remove(key));
        public Task RemoveByPatternAsync(string pattern)
        {
            
            return Task.CompletedTask;
        }

        public Task RemoveCacheGroupAsync(string cacheGroupKey)
        {
            
            return RemoveByPatternAsync(cacheGroupKey + "*");
        }
    }
}
