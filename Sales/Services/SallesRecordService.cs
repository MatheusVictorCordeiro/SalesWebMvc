using Microsoft.EntityFrameworkCore;
using Sales.Data;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Services
{
    public class SallesRecordService
    {

        private readonly SalesContext _context;

        public SallesRecordService(SalesContext context)
        {

            _context = context;
        }

        public async Task<List<SallesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //construindo um objeto Iqueryable, podemos construir consultas em cima dele.
            var result = from obj in _context.SallesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);

            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result

                //fazendo Join em outras tabelas.
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

        }

        public async Task<List<IGrouping<Department,SallesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            //construindo um objeto Iqueryable, podemos construir consultas em cima dele.
            var result = from obj in _context.SallesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);

            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result

                //fazendo Join em outras tabelas.
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x=> x.Seller.Department)
                .ToListAsync();

        }
    }
}
