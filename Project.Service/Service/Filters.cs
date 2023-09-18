using Project.Service.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public class Filters
    {
        public Filtering Filtering { get; set; }
        public Sorting Sorting { get; set; }
        public Pagination Pagination { get; set; }

        public Filters()
        {
            Filtering = new Filtering();
            Sorting = new Sorting();
            Pagination = new Pagination();
        }
    }
}
