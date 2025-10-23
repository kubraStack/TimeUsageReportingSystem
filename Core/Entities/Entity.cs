using Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public  class Entity : IEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
