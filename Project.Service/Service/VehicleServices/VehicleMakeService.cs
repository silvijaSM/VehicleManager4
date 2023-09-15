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
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly ProjectServiceContext _context;

        public VehicleMakeService(ProjectServiceContext context)
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

        public bool VehicleMakeExists(int id)
        {
            return _context.VehicleMake.Any(make => make.Id == id);
        }
    }
}
