using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Models.Entity;
using Project.Service.Service.VehicleServices;
using Project.Service.Service;
using Project.MVC.Models.ViewModels;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;
        private readonly Filters _filters;
        private readonly Sorting _sorting;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper, Filters filters, Sorting sorting)
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
            _filters = filters;
            _sorting = sorting;
        }

        // GET: VehicleMake
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["AbrvSortParm"] = sortOrder == "Abrv" ? "Abrv_desc" : "Abrv";

            if (!string.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
            }

            try
            {
                int pageSize = 10;
                pageNumber ??= 1;

                var filters = new Filters
                {
                    Sorting = new Sorting { SortOrder = sortOrder },
                    Filtering = new Filtering { SearchString = searchString }
                };

                var paginatedMakes = await _vehicleMakeService.GetFilteredAndSortedMakesAsync(
                    filters, searchString, sortOrder, pageNumber.Value, pageSize);

                var modelViewMakes = _mapper.Map<List<VehicleMake>, List<VehicleMakeView>>(paginatedMakes);

                var paginatedViewModels = new PaginatedList<VehicleMakeView>(modelViewMakes, paginatedMakes.TotalItems, pageNumber.Value, pageSize);

                Console.WriteLine($"Sorting order: {sortOrder}");
                Console.WriteLine($"Count of paginatedMakes: {paginatedMakes.Count()}");

                foreach (var item in paginatedMakes)
                {
                    Console.WriteLine($"Name: {item.Name}, Abrv: {item.Abrv}");
                }

                return View(paginatedViewModels);

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