using Cosmos.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cosmos.Controllers
{
    public class FlightController : Controller
    {
        private readonly CosmosDataContext context;

        public FlightController(CosmosDataContext context)
        {
            this.context = context;
        }
        //public IActionResult Flights(int? id)
        //{   
        //    if(id == null)
        //    {
        //        throw new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Route route = 
        //    return View();
        //}
    }
}
