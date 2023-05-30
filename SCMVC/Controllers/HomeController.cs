using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCMVC.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace SCMVC.Controllers
{
    public class HomeController : Controller
    {
       

        public HomeController( )
        {        
            
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }

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