﻿using DALayer.Entities;

namespace BLLayer.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task <T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
