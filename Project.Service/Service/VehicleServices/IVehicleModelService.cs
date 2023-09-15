using Project.Service.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service.VehicleServices
{
    public interface IVehicleModelService
    {
        IQueryable<VehicleModel> GetAllVehicleModels();
        VehicleModel? GetVehicleModelById(int id);
        void CreateVehicleModel(VehicleModel model);
        void UpdateVehicleModel(VehicleModel model);
        void DeleteVehicleModel(int id);
        bool VehicleModelExists(int id);
    }
}
