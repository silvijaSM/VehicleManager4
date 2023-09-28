using Project.MVC;
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
        Task<IQueryable<VehicleModel>> GetAllVehicleModelsAsync();
        Task<PaginatedList<VehicleModel>> GetFilteredAndSortedModelsAsync(Filters filters, string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<VehicleModel?> GetVehicleModelByIdAsync(int id);
        Task CreateVehicleModelAsync(VehicleModel model);
        Task UpdateVehicleModelAsync(VehicleModel model);
        Task DeleteVehicleModelAsync(int id);
        Task<bool> VehicleModelExistsAsync(int id);
    }
}
