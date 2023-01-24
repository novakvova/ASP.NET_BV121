using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWeb.Data.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        bool IsDelete { get; set; }
        DateTime DateCreated { get; set; }
    }

    public abstract class BaseEntity<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
