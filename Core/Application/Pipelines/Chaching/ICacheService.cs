using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Chaching
{
    public interface ICacheService
    {
        //Veri Ekleme Asenkron
        Task AddAsync(string key, object value, int duration);

        //Veri ekleme senkron
        void Add(string key, object value, int duration);

        //Veri çekme (Asenkron - Deserialization işlemi içerir
        Task<T> GetAsync<T>(string key);

        //Veri çekme senkron
        object Get(string key);

        //Anahatarın önbellekte olup olmadığını kontrol etme
        bool IsAdd(string key);

        //Belirli bir anahtarı kaldırma 
        Task RemoveAsync(string key);

        //Bir desene uyan anahtarları kaldırma (örn: user.*)
        Task RemoveByPatternAsync(string pattern);

        //Cache Group Temizleme
        Task RemoveCacheGroupAsync(string cacheGroupKey);
    }
}
