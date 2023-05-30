using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using SCMVC.Models;
using System;
using System.Collections.Generic;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using static NuGet.Packaging.PackagingConstants;
using static System.Net.WebRequestMethods;

namespace SCMVC.Controllers
{
    public class OrderController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7084/api/Order/");
         
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

         
        public IActionResult Edit(int? id)
        {
            Order order = new Order();
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + id.ToString()).Result;
            string data;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                order = JsonConvert.DeserializeObject<Order>(data);
            }
            
            return View(order);
        }

        [HttpPut, ActionName("Edit")]
        public async Task<IActionResult> Edit()
        {
            Order order = new Order();
        
            await httpClient.PutAsJsonAsync(baseAddress, order);
            
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            Order order = new Order();
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(baseAddress + id.ToString()).Result;
            string data;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                order = JsonConvert.DeserializeObject<Order>(data);
            }
            return View(order);
        }
         
        [HttpDelete, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
