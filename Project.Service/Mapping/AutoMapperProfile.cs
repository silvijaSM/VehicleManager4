using AutoMapper;
using Project.Service.Models.Entity;
using Project.Service.Models.ViewModels;


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
