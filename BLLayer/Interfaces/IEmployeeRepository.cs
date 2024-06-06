﻿using BLLayer.Repositiories;
using DALayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
     Task<IEnumerable<Employee> >Search(string name);
    }
}
