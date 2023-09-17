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