using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Carwale.Models
{

    public class VehicleModel
    {
        public int Id { get; set; }
        [Required]
        public string modelName { get; set; }
        [Required]
        public string brand { get; set; }
        [Required]
        public int? price { get; set; }
        [Required]
        public string mileage { get; set; }
        [Required]
        public string engine { get; set; }
        [Required]
        public string transmission { get; set; }
        [Required(ErrorMessage = "Please Enter the Fuel Type (Ex: Petrol, Diesel)")]
        public string fuelType { get; set; }
        [Required]
        public string capacity { get; set; }

        [Required]
        public IFormFile vehiclePhoto { get; set; }

        public string vehicleURI { get; set; }
    }
}