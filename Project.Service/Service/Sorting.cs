using System;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Service.Service
{
    public class Sorting
    {
        public string? SortOrder { get; set; }

        public IQueryable<T> ApplySorting<T, TKey>(IQueryable<T> query,string sortOrder,Expression<Func<T, TKey>> orderBy)
        {
            SortOrder = sortOrder;

            return sortOrder switch
            {
                "name_desc" => query.OrderByDescending(orderBy),
                "Name" => query.OrderBy(orderBy),
                "abrv_desc" => query.OrderByDescending(orderBy),
                "Abrv" => query.OrderBy(orderBy),
                _ => query.OrderBy(orderBy),
            };
        }
    }
}
