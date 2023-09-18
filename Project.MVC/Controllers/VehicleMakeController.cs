using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Models.Entity;
using Project.Service.Models.ViewModels;
using Project.Service.Service.VehicleServices;
using Project.Service.Service;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;
        private readonly Filters _filters;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper, Filters filters)
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
            _filters = filters;
        }

        // GET: VehicleMake
        public async Task<IActionResult> Index()
        {
            try
            {
                var allMakes = await _vehicleMakeService.GetAllVehicleMakesAsync();
                var makeViewModels = _mapper.Map<List<VehicleMakeView>>(allMakes.ToList());

                var vehicleMakeViewModel = new VehicleMakeView
                {
                    Makes = makeViewModels,
                    Filters = new Filters()
                };

                return View(vehicleMakeViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: VehicleMake/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleMakeView makeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var make = _mapper.Map<VehicleMakeView, VehicleMake>(makeViewModel);
                    _vehicleMakeService.CreateVehicleMakeAsync(make);
                    return RedirectToAction(nameof(Index));
                }

                return View(makeViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: VehicleMakes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var make = await _vehicleMakeService.GetVehicleMakeByIdAsync(id);
                if (make == null)
                {
                    return NotFound();
                }

                var makeViewModel = _mapper.Map<VehicleMake, VehicleMakeView>(make);
                return View(makeViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: VehicleMakes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, VehicleMakeView makeViewModel)
        {
            try
            {
                if (id != makeViewModel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var make = _mapper.Map<VehicleMakeView, VehicleMake>(makeViewModel);
                    _vehicleMakeService.UpdateVehicleMakeAsync(make);
                    return RedirectToAction(nameof(Index));
                }

                return View(makeViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: VehicleMakes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var make = await _vehicleMakeService.GetVehicleMakeByIdAsync(id);
                if (make == null)
                {
                    return NotFound();
                }

                var makeViewModel = _mapper.Map<VehicleMake, VehicleMakeView>(make);
                return View(makeViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _vehicleMakeService.DeleteVehicleMakeAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: VehicleMake/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var make = await _vehicleMakeService.GetVehicleMakeByIdAsync(id);
                if (make == null)
                {
                    return NotFound();
                }

                var makeViewModel = _mapper.Map<VehicleMake, VehicleMakeView>(make);
                return View(makeViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}