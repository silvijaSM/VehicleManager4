using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using Project.Service.Service;
using System;
using System.Collections.Generic;
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
            int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var vehicleModels = _vehicleService.GetAllVehicleModels();

            // Apply filtering
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModels = (List<VehicleModel>)vehicleModels.Where(model => model.Name.Contains(searchString) || model.Abrv.Contains(searchString));
            }

            // Apply sorting
            vehicleModels = sortOrder switch
            {
                "name_desc" => vehicleModels.OrderByDescending(model => model.Name),
                "Abrv" => vehicleModels.OrderBy(model => model.Abrv),
                "abrv_desc" => vehicleModels.OrderByDescending(model => model.Abrv),
                _ => vehicleModels.OrderBy(model => model.Name),
            };

            int pageSize = 10;

            return View(await PaginatedList<VehicleModel>.CreateAsync(vehicleModels.AsQueryable(), pageNumber ?? 1, pageSize));
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
            return View();
        }

        // POST: VehicleModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MakeID,Name,Abrv")] VehicleModel vehicleModel)
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

        // POST: VehicleModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,MakeID,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _vehicleService.UpdateVehicleModel(vehicleModel);
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

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _vehicleService.DeleteVehicleModel(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleModelExists(int id)
        {
            var vehicleModel = _vehicleService.GetVehicleModelById(id);
            return vehicleModel != null;
        }
    }
}
