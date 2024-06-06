using BLLayer.Interfaces;
using DALayer.Context;
using DALayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLLayer.Repositiories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task <IEnumerable<Employee>> Search(string name)
        {
            var result = _context.Employeesss.Where(employee =>
                                                   employee.Name.Trim().ToLower().Contains(name.Trim().ToLower())  ||
                                                   employee.Email.Trim().ToLower().Contains(name.Trim().ToLower())

            );
            return await result.ToListAsync();
        }
    }
}