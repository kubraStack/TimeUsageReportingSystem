using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Chaching
{
    public interface ICacheRemoverRequest
    {
        //Önbelleği temizlemek isteyen komutlar için ana arayüz

        // Temizlenecek belirli bir önbellek anahtarı
        public string CacheKey { get;}

        // Önbellek temizleme işlemini atlamak için kullanılır (Nadir durumlarda)
        public bool BypassCache { get;}
        // Temizlenecek ilgili önbellek grubu (örneğin: "TimeLogList", "EmployeeList")
        public string? CacheGroupKey { get; }

    }
}
