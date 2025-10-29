using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Paging
{
    public class PageRequest
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
