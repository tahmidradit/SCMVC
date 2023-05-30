using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using SCMVC.Models;
using System;
using System.Collections.Generic;
using static NuGet.Packaging.PackagingConstants;
using static System.Net.WebRequestMethods;

namespace SCMVC.Controllers
{
    public class OrderController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7084/api/Order");
         
        private readonly HttpClient httpClient;

        public OrderController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
              
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Order> orders = new List<Order>();
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress).Result;
            string data;
            if(httpResponseMessage.IsSuccessStatusCode)
            {
                data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                orders = JsonConvert.DeserializeObject<List<Order>>(data);
            }
            return View(orders);
        }

        
        public IActionResult Create() 
        {
            return View();
        }
        
        [HttpPost, ActionName("Create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST(Order order)
        {
            await httpClient.PostAsJsonAsync(baseAddress, order);
            //await response.Content.ReadFromJsonAsync<Order>(); 
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            //var findById = await context.Categories.FindAsync(id);
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress).Result;


            return View();
        }

        //[HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        //public async Task<IActionResult> DeletePOST(int? id)
        //{
        //    var findById = await context.Categories.FindAsync(id);

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    if (findById == null)
        //    {
        //        return View();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        context.Categories.Remove(findById);
        //        await context.SaveChangesAsync();
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
