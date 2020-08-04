using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
   public interface IserviceRepository<T> where T :BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
