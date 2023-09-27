using Project.MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MVC.Models.ViewModels
{
    public class VehicleModelView
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Abrv { get; set; }
        public int MakeID { get; set; }
    }
}
