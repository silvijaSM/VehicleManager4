using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models;
using Project.Service.Service;

namespace Project.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleMakesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: VehicleMakes
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            string selectedMake, 
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var vehicleMakes = _vehicleService.GetAllVehicleMakes();

            if (!string.IsNullOrEmpty(selectedMake))
            {
                vehicleMakes = (List<VehicleMake>)vehicleMakes.Where(make => make.Name == selectedMake);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(make => make.Name);
                    break;
                case "abrv":
                    vehicleMakes = vehicleMakes.OrderBy(make => make.Abrv);
                    break;
                case "abrv_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(make => make.Abrv);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(make => make.Name);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<VehicleMake>.CreateAsync(vehicleMakes.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: VehicleMakes/Details/5
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IActionResult> Details(int? id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = _vehicleService.GetVehicleMakeById(id.Value);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (ModelState.IsValid)
            {
                _vehicleService.CreateVehicleMake(vehicleMake);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = _vehicleService.GetVehicleMakeById(id.Value);

            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vehicleService.UpdateVehicleMake(vehicleMake);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_vehicleService.VehicleMakeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = _vehicleService.GetVehicleMakeById(id.Value);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            _vehicleService.DeleteVehicleMake(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
