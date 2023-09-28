using Microsoft.EntityFrameworkCore;
using Project.MVC;
using Project.Service.Data;
using Project.Service.Models.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Service.Service.VehicleServices
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly ProjectServiceContext _context;

        public VehicleModelService(ProjectServiceContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<VehicleModel>> GetFilteredAndSortedModelsAsync(Filters filters,
            string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.VehicleModel.AsQueryable();

            if (filters != null)
            {
                if (!string.IsNullOrEmpty(filters.Filtering.SearchString))
                {
                    query = query.Where(model => model.Name != null && model.Name.Contains(filters.Filtering.SearchString));
                }

                if (!string.IsNullOrEmpty(sortOrder) && filters != null)
                {
                    if (sortOrder == "name")
                    {
                        query = filters.Sorting.ApplySorting(query, sortOrder, model => model.Name);
                    }
                    else if (sortOrder == "abrv")
                    {
                        query = filters.Sorting.ApplySorting(query, sortOrder, model => model.Abrv);
                    }
                }
            }

            var count = await query.CountAsync();

            var items = await PaginatedList<VehicleModel>.CreateAsync(query, pageNumber, pageSize);

            return items;
        }

        public async Task<IQueryable<VehicleModel>> GetAllVehicleModelsAsync()
        {
            return await Task.FromResult(_context.VehicleModel.AsQueryable());
        }

        public async Task<VehicleModel?> GetVehicleModelByIdAsync(int id)
        {
            return await _context.VehicleModel.FindAsync(id);
        }

        public async Task CreateVehicleModelAsync(VehicleModel model)
        {
            _context.VehicleModel.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicleModelAsync(VehicleModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleModelAsync(int id)
        {
            var model = await _context.VehicleModel.FindAsync(id);
            if (model != null)
            {
                _context.VehicleModel.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> VehicleModelExistsAsync(int id)
        {
            return await _context.VehicleModel.AnyAsync(model => model.Id == id);
        }

        public async Task<bool> VehicleMakeExistsAsync(int id)
        {
            return await _context.VehicleMake.AnyAsync(make => make.Id == id);
        }
    }
}
