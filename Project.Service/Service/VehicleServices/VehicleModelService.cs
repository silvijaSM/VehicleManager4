using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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