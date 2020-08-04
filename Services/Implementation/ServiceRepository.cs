using DataLayer;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class ServiceRepository<T> : IserviceRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> Table;
        public ServiceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Table = _dbContext.Set<T>();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }

        public T Get(long id)
        {
            return Table.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Table.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
