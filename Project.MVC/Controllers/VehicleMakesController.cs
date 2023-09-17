using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Models.ViewModels;
using Project.Service.Service.VehicleServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var makes = _vehicleMakeService.GetAllVehicleMakes();
            var makeViewModels = _mapper.Map<IEnumerable<VehicleMakeView>>(makes);
            return View(makeViewModels);
        }

        // Add Create, Edit, Delete actions as needed
    }
}
