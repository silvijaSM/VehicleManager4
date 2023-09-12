using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models;

namespace Project.Service.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly ProjectServiceContext _context;

        public VehicleService(ProjectServiceContext context)
        {
            _context = context;
        }

        public IQueryable<VehicleMake> GetAllVehicleMakes()
        {
            return _context.VehicleMake.AsQueryable();
        }

        public VehicleMake? GetVehicleMakeById(int id)
        {
            return _context.VehicleMake.Find(id);
        }

        public void CreateVehicleMake(VehicleMake make)
        {
            _context.VehicleMake.Add(make);
            _context.SaveChanges();
        }

        public void UpdateVehicleMake(VehicleMake make)
        {
            _context.Entry(make).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteVehicleMake(int id)
        {
            var make = _context.VehicleMake.Find(id);
            if (make != null)
            {
                _context.VehicleMake.Remove(make);
                _context.SaveChanges();
            }
        }

        public IQueryable<VehicleModel> GetAllVehicleModels()
        {
            return _context.VehicleModel.AsQueryable();
        }

        public VehicleModel? GetVehicleModelById(int id)
        {
            return _context.VehicleModel.Find(id);
        }

        public void CreateVehicleModel(VehicleModel model)
        {
            _context.VehicleModel.Add(model);
            _context.SaveChanges();
        }

        public void UpdateVehicleModel(VehicleModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteVehicleModel(int id)
        {
            var model = _context.VehicleModel.Find(id);
            if (model != null)
            {
                _context.VehicleModel.Remove(model);
                _context.SaveChanges();
            }
        }

        public bool VehicleModelExists(int id)
        {
            return _context.VehicleModel.Any(model => model.Id == id);
        }

        public bool VehicleMakeExists(int id)
        {
            return _context.VehicleMake.Any(make => make.Id == id);
        }
    }
}
