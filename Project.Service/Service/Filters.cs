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
        public Filtering<VehicleMake> MakeFiltering { get; set; }
        public Filtering<VehicleModel> ModelFiltering { get; set; }
        public Sorting Sorting { get; set; }
        public Pagination Pagination { get; set; }

        public Filters()
        {
            MakeFiltering = new Filtering<VehicleMake>();
            ModelFiltering = new Filtering<VehicleModel>();
            Sorting = new Sorting();
            Pagination = new Pagination();
        }
    }
}
