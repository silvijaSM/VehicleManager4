using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Service.Models.Entity;
using Project.Service.Service.VehicleServices;

namespace Project.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleMakeService _vehicleService;

        public VehicleMakesController(IVehicleMakeService vehicleService)
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

            var vehicleMakesQuery = _vehicleService.GetAllVehicleMakes().AsQueryable();

            if (!string.IsNullOrEmpty(selectedMake))
            {
                vehicleMakesQuery = vehicleMakesQuery.Where(make => make.Name == selectedMake);
            }

            vehicleMakesQuery = sortOrder switch
            {
                "name_desc" => vehicleMakesQuery.OrderByDescending(make => make.Name),
                "abrv" => vehicleMakesQuery.OrderBy(make => make.Abrv),
                "abrv_desc" => vehicleMakesQuery.OrderByDescending(make => make.Abrv),
                _ => vehicleMakesQuery.OrderBy(make => make.Name),
            };

            int pageSize = 10;
            return View(await PaginatedList<VehicleMake>.CreateAsync(vehicleMakesQuery, pageNumber ?? 1, pageSize));
        }

        // GET: VehicleMakes/Details/5
        public IActionResult Details(int? id)
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
