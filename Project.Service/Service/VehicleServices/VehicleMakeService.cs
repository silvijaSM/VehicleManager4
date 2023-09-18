using Project.Service.Data;
using Project.Service.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Service.Service.VehicleServices
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly ProjectServiceContext _context;

        public VehicleMakeService(ProjectServiceContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleMake>> GetFilteredAndSortedMakesAsync(Filters filters)
        {
            var query = _context.VehicleMake.AsQueryable();


            if (!string.IsNullOrEmpty(filters.Filtering.SearchString))
            {
                query = query.Where(make => make.Name != null && make.Name.Contains(filters.Filtering.SearchString));
            }

            int totalItems = await query.CountAsync();

            if (!string.IsNullOrEmpty(filters.Sorting.SortOrder))
            {
                switch (filters.Sorting.SortOrder)
                {
                    case "Name":
                        query = query.OrderBy(make => make.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(make => make.Name);
                        break;
                    case "Abrv":
                        query = query.OrderBy(make => make.Abrv);
                        break;
                    case "abrv_desc":
                        query = query.OrderByDescending(make => make.Abrv);
                        break;
                }
            }

            query = query.Skip((filters.Pagination.PageNumber - 1) * filters.Pagination.PageSize)
                         .Take(filters.Pagination.PageSize);

            var items = await query.ToListAsync();

            return await query.ToListAsync();
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