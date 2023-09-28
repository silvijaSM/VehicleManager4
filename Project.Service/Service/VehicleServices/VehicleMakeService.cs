using Project.Service.Data;
using Project.Service.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Project.MVC;

namespace Project.Service.Service.VehicleServices
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly ProjectServiceContext _context;

        public VehicleMakeService(ProjectServiceContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<VehicleMake>> GetFilteredAndSortedMakesAsync(Filters filters,
            string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.VehicleMake.AsQueryable();

            if (filters != null)
            {
                if (!string.IsNullOrEmpty(filters.Filtering.SearchString))
                {
                    query = query.Where(make => make.Name != null && make.Name.Contains(filters.Filtering.SearchString));
                }

                if (!string.IsNullOrEmpty(sortOrder) && filters != null)
                {
                    if (sortOrder == "name")
                    {
                        query = filters.Sorting.ApplySorting(query, sortOrder, make => make.Name);
                    }
                    else if (sortOrder == "abrv")
                    {
                        query = filters.Sorting.ApplySorting(query, sortOrder, make => make.Abrv);
                    }
                }
            }

            var count = await query.CountAsync();

            var items = await PaginatedList<VehicleMake>.CreateAsync(query, pageNumber, pageSize);

            return items;
        }


        public async Task<IQueryable<VehicleMake>> GetAllVehicleMakesAsync()
        {
            return await Task.FromResult(_context.VehicleMake.AsQueryable());
        }

        public async Task<VehicleMake?> GetVehicleMakeByIdAsync(int id)
        {
            return await _context.VehicleMake.FindAsync(id);
        }

        public async Task CreateVehicleMakeAsync(VehicleMake make)
        {
            _context.VehicleMake.Add(make);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicleMakeAsync(VehicleMake make)
        {
            _context.Entry(make).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleMakeAsync(int id)
        {
            var make = await _context.VehicleMake.FindAsync(id);
            if (make != null)
            {
                _context.VehicleMake.Remove(make);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> VehicleMakeExistsAsync(int id)
        {
            return await _context.VehicleMake.AnyAsync(make => make.Id == id);
        }
    }
}