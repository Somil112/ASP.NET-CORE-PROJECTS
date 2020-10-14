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

namespace Carwale
{
    public class HomeController : Controller
    {

        [ViewData]
        public string Title { get; set; }
        public ViewResult Index()
        {

            Title = "Welcome";
            return View();
        }

    }
}