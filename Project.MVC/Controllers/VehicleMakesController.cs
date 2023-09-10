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

            var vehicleMakes = _vehicleService.GetAllVehicleMakes();

            // Apply filtering
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakes = (List<VehicleMake>)vehicleMakes.Where(make => make.Name.Contains(searchString) || make.Abrv.Contains(searchString));
            }

            // Apply sorting
            vehicleMakes = sortOrder switch
            {
                "name_desc" => vehicleMakes.OrderByDescending(make => make.Name),
                "Abrv" => vehicleMakes.OrderBy(make => make.Abrv),
                "abrv_desc" => vehicleMakes.OrderByDescending(make => make.Abrv),
                _ => vehicleMakes.OrderBy(make => make.Name),
            };

            int pageSize = 10;

            return View(await PaginatedList<VehicleMake>.CreateAsync(vehicleMakes.AsQueryable(), pageNumber ?? 1, pageSize));
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

        // POST: VehicleMakes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Abrv")] VehicleMake vehicleMake)
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

        // POST: VehicleMakes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _vehicleService.UpdateVehicleMake(vehicleMake);
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

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _vehicleService.DeleteVehicleMake(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            var vehicleMake = _vehicleService.GetVehicleMakeById(id);
            return vehicleMake != null;
        }
    }
}
