using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Paging
{
    public class ListModel<T>
    {
        public List<T> Items { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }

        public ListModel()
        {
            Items = new List<T>();
        }

    }
}
