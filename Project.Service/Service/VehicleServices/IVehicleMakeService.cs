using Project.Service.Models.Entity;
using Project.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service.VehicleServices
{
    public interface IVehicleMakeService
    {
        Task<IQueryable<VehicleMake>> GetAllVehicleMakesAsync();
        Task<List<VehicleMake>> GetFilteredAndSortedMakesAsync(Filters filters);
        Task<VehicleMake?> GetVehicleMakeByIdAsync(int id);
        Task CreateVehicleMakeAsync(VehicleMake make);
        Task UpdateVehicleMakeAsync(VehicleMake make);
        Task DeleteVehicleMakeAsync(int id);
        Task<bool> VehicleMakeExistsAsync(int id);
    }
}
