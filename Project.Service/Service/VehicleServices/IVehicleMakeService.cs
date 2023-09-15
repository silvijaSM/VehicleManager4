using Project.Service.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service.VehicleServices
{
    public interface IVehicleMakeService
    {
        IQueryable<VehicleMake> GetAllVehicleMakes();
        VehicleMake? GetVehicleMakeById(int id);
        void CreateVehicleMake(VehicleMake make);
        void UpdateVehicleMake(VehicleMake make);
        void DeleteVehicleMake(int id);
        bool VehicleMakeExists(int id);
    }
}
