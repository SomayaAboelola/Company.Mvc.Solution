using BLLayer.Interfaces;
using DALayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Repositiories
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _context;

        public IDepartmentRepository DepartmentRepository { get ; set ; }
        public IEmployeeRepository EmployeeRepository { get; set ; }
    

        public UnitofWork(AppDbContext context)
        {
            DepartmentRepository = new DepartmentRepository(context);
            EmployeeRepository   = new EmployeeRepository(context);
            _context = context;
        }  
        public async Task <int> completeAsync ()
        { 
            return await _context.SaveChangesAsync();  
        }
    }
}