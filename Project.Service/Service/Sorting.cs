using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public class Sorting
    {
        public string? SortOrder { get; set; }
        public IQueryable<T> ApplySorting<T>(IQueryable<T> query, string sortOrder, Func<T, object> orderBy)
        {
            SortOrder = sortOrder;

            return sortOrder switch
            {
                "name_desc" => (IQueryable<T>)query.OrderByDescending(orderBy),
                "Name" => (IQueryable<T>)query.OrderBy(orderBy),
                "abrv_desc" => (IQueryable<T>)query.OrderByDescending(orderBy),
                "Abrv" => (IQueryable<T>)query.OrderBy(orderBy),
                _ => (IQueryable<T>)query.OrderBy(orderBy),
            };
        }
    }
}
