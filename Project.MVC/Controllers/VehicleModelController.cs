using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Models.Entity;
using Project.Service.Models.ViewModels;
using Project.Service.Service.VehicleServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IMapper _mapper;

        public VehicleModelController(IVehicleModelService vehicleModelService, IMapper mapper)
        {
            _vehicleModelService = vehicleModelService;
            _mapper = mapper;
        }

        // GET: VehicleModel
        public async Task<IActionResult> Index()
        {
            try
            {
                var models = await _vehicleModelService.GetAllVehicleModelsAsync();
                var modelViewModels = _mapper.Map<List<VehicleModelView>>(models.ToList());

                return View(modelViewModels);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: VehicleModel/Create
        public IActionResult Create(int makeId)
        {
            ViewBag.MakeID = makeId;

            var model = new VehicleModelView();

            return View(model);
        }

        // POST: VehicleModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModelView modelViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _mapper.Map<VehicleModelView, VehicleModel>(modelViewModel);
                    await _vehicleModelService.CreateVehicleModelAsync(model);
                    return RedirectToAction(nameof(Index));
                }

                return View(modelViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: VehicleModel/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await _vehicleModelService.GetVehicleModelByIdAsync(id);
                if (model == null)
                {
                    return NotFound();
                }

                var modelViewModel = _mapper.Map<VehicleModel, VehicleModelView>(model);
                return View(modelViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: VehicleModel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleModelView modelViewModel)
        {
            try
            {
                if (id != modelViewModel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var model = _mapper.Map<VehicleModelView, VehicleModel>(modelViewModel);
                    await _vehicleModelService.UpdateVehicleModelAsync(model);
                    return RedirectToAction(nameof(Index));
                }

                return View(modelViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: VehicleModel/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _vehicleModelService.GetVehicleModelByIdAsync(id);
                if (model == null)
                {
                    return NotFound();
                }

                var modelViewModel = _mapper.Map<VehicleModel, VehicleModelView>(model);
                return View(modelViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: VehicleModel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _vehicleModelService.DeleteVehicleModelAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: VehicleModel/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var model = await _vehicleModelService.GetVehicleModelByIdAsync(id);
                if (model == null)
                {
                    return NotFound();
                }

                var modelViewModel = _mapper.Map<VehicleModel, VehicleModelView>(model);
                return View(modelViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}
