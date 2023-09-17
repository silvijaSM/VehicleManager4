using AutoMapper;
using Project.Service.Models.Entity;
using Project.MVC.Models.ViewModels;


namespace Project.Service.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeView>();
            CreateMap<VehicleModel, VehicleModelView>();
        }
    }
}
