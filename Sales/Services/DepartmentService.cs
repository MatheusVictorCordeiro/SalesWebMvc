using Microsoft.EntityFrameworkCore;
using Sales.Data;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Sales.Services
{
    public class DepartmentService
    {
        private readonly SalesContext _context;

        public DepartmentService(SalesContext context)
        {
            _context = context;
        }
        //Assincrôno
        public async Task<List<Department>> FindAllAsync()
        {

            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

    }
}
