using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VolvoTrucks.Domain;
using VolvoTrucks.Services;
using VolvoTrucks.WebApp.Models;

namespace VolvoTrucks.WebApp.Controllers
{
    public class TrucksController : Controller
    {
        private readonly ITruckService _service;

        public TrucksController(ITruckService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ListTrucks");
        }

        #region List Trucks and Models

        public IActionResult ListModels()
        {
            var res = _service.ListModels();
            return View("ListModels", res);
        }

        public IActionResult ListTrucks()
        {
            var trucks = _service.ListAllTrucks();
            List<TruckViewModel> res = new List<TruckViewModel>();
            trucks.ForEach(t =>
            {
                TruckViewModel tvm = new TruckViewModel()
                {
                    TruckId = t.TruckId,
                    Description = t.Description,
                    ManufacturingYear = t.ManufacturingYear,
                    ModelYear = t.ModelYear,
                    ModelId = t.Model?.TruckModelId,
                    ModelName = t.Model?.Model
                };
                res.Add(tvm);
            });
            return View("ListTrucks", res);
        }

        #endregion

        #region Create Truck

        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.Models = new SelectList(_service.ListAvailableModels(), "TruckModelId", "Model");
            ViewBag.Years = new[]
            {
                new SelectListItem() { Value = DateTime.Now.Year.ToString(), Text = DateTime.Now.Year.ToString() },
                new SelectListItem() { Value = (DateTime.Now.Year+1).ToString(), Text = (DateTime.Now.Year+1).ToString() }
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] TruckViewModel truck)
        {
            if (ModelState.IsValid)
            {
                var truckModel = _service.FindModelById((int)truck.ModelId);
                var newTruck = new Truck()
                {
                    ModelYear = truck.ModelYear,
                    Description = truck.Description,
                    ManufacturingYear = truck.ManufacturingYear,
                    Model = truckModel
                };
                _service.SaveOrUpdateTruck(newTruck);
            }

            return RedirectToAction("ListTrucks");
        }

        #endregion

        #region Delete Truck
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var truck = _service.FindTruckById(id);
            if (truck != null)
            {
                var truckmodel = new TruckViewModel(truck);
                return View(truckmodel);
            }
            return RedirectToAction("Error");            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind] TruckViewModel truck)
        {
            if (truck != null && truck.TruckId != null && truck.TruckId != 0)
            {
                _service.DeleteTruck((int)truck.TruckId);
            }

            return RedirectToAction("ListTrucks");
        }

        #endregion

        #region Update Truck
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Models = new SelectList(_service.ListAvailableModels(), "TruckModelId", "Model");
            ViewBag.Years = new[]
{
                new SelectListItem() { Value = DateTime.Now.Year.ToString(), Text = DateTime.Now.Year.ToString() },
                new SelectListItem() { Value = (DateTime.Now.Year+1).ToString(), Text = (DateTime.Now.Year+1).ToString() }
            };

            var truck = _service.FindTruckById(id);
            var truckVM = new TruckViewModel(truck);

            return View("Edit", truckVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] TruckViewModel updated)
        {
            var truck = _service.FindTruckById((int)updated.TruckId);
            if (truck.TruckModelId != updated.ModelId)
            {
                truck.TruckModelId = (int)updated.ModelId;
                var model = _service.FindModelById((int)updated.ModelId);
                truck.Model = model;
            }
            truck.Description = updated.Description;
            truck.ManufacturingYear = updated.ManufacturingYear;
            truck.ModelYear = updated.ModelYear;
            _service.SaveOrUpdateTruck(truck);
            return RedirectToAction("ListTrucks");
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
