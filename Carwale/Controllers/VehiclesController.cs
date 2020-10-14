using System.Runtime.InteropServices.WindowsRuntime;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Carwale.Repository;
using Carwale.Models;
using System.IO;
namespace Carwale
{
    public class VehiclesController : Controller
    {
        // private readonly VehicleRepository _vehicleRepository = null;"server=localhost;port=3306;database=test;user=root;password=Somil@112"
        [ViewData]
        public string Title { get; set; }
        public AppDb Db { get; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        private List<String> getFuelType()
        {
            return new List<String> { "Petrol", "Diesel", "Electric" };
        }

        public VehiclesController(AppDb db, IWebHostEnvironment webHostEnvironment)
        {
            Db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public ViewResult AddVehicle(bool IsSuccess = false)
        {
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.fuelType = getFuelType();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle(VehicleModel vehicle)
        {

            Title = "Add Vehicle";
            if (ModelState.IsValid)
            {

                if (vehicle.vehiclePhoto != null)
                {
                    string folder = "vehicles/";
                    folder += Guid.NewGuid().ToString() + vehicle.vehiclePhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await vehicle.vehiclePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    vehicle.vehicleURI = folder;
                }
                await Db.Connection.OpenAsync();
                var query = new VehicleRepository(Db);
                bool IsSuccess = await query.AddNewVehicle(vehicle);
                if (IsSuccess)
                {
                    return RedirectToAction(nameof(AddVehicle), new { IsSuccess });
                }
            }
            ViewBag.IsSuccess = false;
            ViewBag.fuelType = getFuelType();

            return View();

        }
        public async Task<ViewResult> GetAllVehicles()
        {
            Title = "Top Brands";
            await Db.Connection.OpenAsync();
            var query = new VehicleRepository(Db);
            var data = await query.GetAllVehicles();
            return View(data);
        }

        public async Task<ViewResult> GetVehicle(int id)
        {

            Title = "Vehicle Detail";
            await Db.Connection.OpenAsync();
            var query = new VehicleRepository(Db);
            var data = await query.GetVehicle(id);
            return View(data);
        }

    }
}