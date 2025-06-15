﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task <IEnumerable<T>> GetAllAsync();
        Task AddAsync (T entity);
        Task DeleteAsync (Guid id);
        Task UpdateAsync (T entity);

    }
}
