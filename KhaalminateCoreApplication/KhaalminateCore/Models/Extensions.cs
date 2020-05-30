using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhaalminateCore.Models
{
#pragma warning disable KApp
    public static class KhaalminateDbContextExtensions
    {
        public static IQueryable<Invoice> GetInvoices(this KhaalminateDbContext dbContext, int pageSize = 10, int pageNumber = 1, int? invid = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            // Get query from DbSet
            var query = dbContext.Invoices.AsQueryable();

            // Filter by: 'LastEditedBy'
            if (invid.HasValue)
                query = query.Where(item => item.Invid == invid);

            return query;
        }

        public static async Task<Invoice> GetInvoiceNumberAsync(this KhaalminateDbContext dbContext, Invoice entity)
            => await dbContext.Invoices.FirstOrDefaultAsync(item => item.Invno == entity.Invno);

        //public static async Task<Invoice> GetInvoicesByInvoiceNameAsync(this KhaalminateDbContext dbContext, Invoice entity)
        //    => await dbContext.Invoices.FirstOrDefaultAsync(item => item. == entity.);
    }

    public static class IQueryableExtensions
    {
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }
#pragma warning restore KApp
}
