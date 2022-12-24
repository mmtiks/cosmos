using Cosmos.Data;
using Cosmos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cosmos.ViewModels;

namespace Cosmos.Controllers
{
    public class HomeController : Controller
    {
        private readonly CosmosDataContext context;

        public HomeController(CosmosDataContext context)
        {
            this.context= context;
        }

        
        public async Task<IActionResult> Index()
        {
                var priceLists = this.context.Pricelist.Select(p => new PriceListView
            {
                id = p.id,
                validUntil = p.validUntil,
                dateAdded = p.dateAdded
            }).ToList();
            return View(priceLists);
        }

        public IActionResult Search(string? id)
        {
            if (id == null)
            {
                return View();
            }
            PriceList priceList = context.Pricelist.Find(id);
            if(priceList == null)
            {
                return View();
            }
            PriceListView other = new PriceListView
            {
                id = priceList.id,
                validUntil = priceList.validUntil,
                dateAdded = priceList.dateAdded
            };
            return View(other);
        }

        private void AddPriceList()
        {
            var priceList = this.context.Pricelist.FirstOrDefault();
            if (priceList != null) return;

            this.context.Pricelist.Add(new PriceList
            {
                id = "3",
                validUntil = DateTime.UtcNow,
                dateAdded = DateTime.UtcNow
            });
            this.context.SaveChanges();
            //Index();
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