using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models.ViewModels
{
    public class VehicleModelView
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Abrv { get; set; }
        public string? Make { get; set; }
    }
}
