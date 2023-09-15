using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Models.Entity;
using Project.Service.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleModelsController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index(
    string sortOrder,
    string currentFilter,
    string searchString,
    string selectedMake,
    int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            var vehicleModelsQuery = _vehicleService.GetAllVehicleModels().AsQueryable();

            if (!string.IsNullOrEmpty(selectedMake))
            {
                vehicleModelsQuery = vehicleModelsQuery.Where(model => model.Name == selectedMake);
            }

            vehicleModelsQuery = sortOrder switch
            {
                "name_desc" => vehicleModelsQuery.OrderByDescending(model => model.Name),
                "abrv" => vehicleModelsQuery.OrderBy(model => model.Abrv),
                "abrv_desc" => vehicleModelsQuery.OrderByDescending(model => model.Abrv),
                _ => vehicleModelsQuery.OrderBy(model => model.Name),
            };

            int pageSize = 10;
            return View(await PaginatedList<VehicleModel>.CreateAsync(vehicleModelsQuery, pageNumber ?? 1, pageSize));
        }

        // GET: VehicleModels/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = _vehicleService.GetVehicleModelById(id.Value);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public IActionResult Create()
        {
            var vehicleMakes = _vehicleService.GetAllVehicleMakes();
            var makeList = new SelectList(vehicleMakes, "Id", "Id");

            ViewData["MakeList"] = makeList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MakeID,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                _vehicleService.CreateVehicleModel(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = _vehicleService.GetVehicleModelById(id.Value);

            if (vehicleModel == null)
            {
                return NotFound();
            }
            return View(vehicleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Make,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vehicleService.UpdateVehicleModel(vehicleModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_vehicleService.VehicleModelExists(id))
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
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = _vehicleService.GetVehicleModelById(id.Value);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _vehicleService.DeleteVehicleModel(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
