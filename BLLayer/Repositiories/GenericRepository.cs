using BLLayer.Interfaces;
using DALayer.Context;
using DALayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLLayer.Repositiories
{
    public class GenericRepository<T> :IGenericRepository<T> where T :BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
             
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
           await _context.Set<T>().ToListAsync();



        public async Task<T?> GetByIdAsync(int id) =>
            await _context.Set<T>().FindAsync(id);



        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
  
        }

       
    }
}
