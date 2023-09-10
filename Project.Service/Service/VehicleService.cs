using Project.Service.Data;
using Project.Service.Models;


namespace Project.Service.Service
{
    public class VehicleService
    {
        private readonly ProjectServiceContext _context;

        public VehicleService(ProjectServiceContext context)
        {
            _context = context;
        }

        public List<VehicleMake> GetAllVehicleMakes()
        {
            return _context.VehicleMake.ToList();
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
            _context.Entry(make).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public List<VehicleModel> GetAllVehicleModels()
        {
            return _context.VehicleModel.ToList();
        }

        public VehicleModel? GetVehicleModelById(int id)
        {
            return _context.VehicleModel.Find(id);
        }

        public void CreateVehicleModel(VehicleModel make)
        {
            _context.VehicleModel.Add(make);
            _context.SaveChanges();
        }

        public void UpdateVehicleModel(VehicleModel make)
        {
            _context.Entry(make).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteVehicleModel(int id)
        {
            var make = _context.VehicleModel.Find(id);
            if (make != null)
            {
                _context.VehicleModel.Remove(make);
                _context.SaveChanges();
            }
        }
    }
}