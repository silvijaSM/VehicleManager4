using Project.Service.Models;

namespace Project.Service.Service
{
    public interface IVehicleService
    {
        List<VehicleMake> GetAllVehicleMakes();
        VehicleMake? GetVehicleMakeById(int id);
        void CreateVehicleMake(VehicleMake make);
        void UpdateVehicleMake(VehicleMake make);
        void DeleteVehicleMake(int id);

        List<VehicleModel> GetAllVehicleModels();
        VehicleModel? GetVehicleModelById(int id);
        void CreateVehicleModel(VehicleModel model);
        void UpdateVehicleModel(VehicleModel model);
        void DeleteVehicleModel(int id);
    }
}
