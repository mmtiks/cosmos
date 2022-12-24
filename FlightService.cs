//using Cosmos.Data;
//using Cosmos.ViewModels;
//using Newtonsoft.Json;
//using Route = Cosmos.Data.Route;

//namespace Cosmos
//{
//    public static class FlightServices
//    {

//        public static void Seed(this IHost host)
//        {
//            using var scope = host.Services.CreateScope();
//            using var context = scope.ServiceProvider.GetRequiredService<CosmosDataContext>();
//            context.Database.EnsureCreated();
//            _ = GetFlights(context, new HttpClient());
//        }



//        private static async Task GetFlights(CosmosDataContext context, HttpClient client)
//        {
//            string response = await client.GetStringAsync("https://cosmos-odyssey.azurewebsites.net/api/v1.0/TravelPrices");
//            ScheduleGet? schedule = JsonConvert.DeserializeObject<ScheduleGet>(response);

//            // Website is up
//            if (schedule == null)
//            {
//                return;
//            }
//            Console.WriteLine(schedule.id);

//            PriceList? priceList = context.Pricelist.FirstOrDefault();
//            Console.WriteLine("still here");

//            // pricelist not already in db
//            if (priceList != null)
//            {
//                return;
//            }
//            Console.WriteLine("mayne here");

//            context.Pricelist.Add(new PriceList
//            {
//                id = schedule.id,
//                validUntil = DateTime.Parse(schedule.validUntil),
//                dateAdded = DateTime.UtcNow
//            });
//            foreach (RouteGet route in schedule.legs)
//            {
//                RouteInfoGet routeinfoget = route.routeinfo;
//                PlaceGet from = routeinfoget.from;
//                PlaceGet to = routeinfoget.to;

//                context.Route.Add(new Route
//                {
//                    id = route.id,
//                    priceListId = schedule.id
//                });

//                context.Place.Add(new Place
//                {
//                    id = from.id,
//                    name = from.name,
//                    routeInfoId = routeinfoget.id
//                });

//                context.Place.Add(new Place
//                {
//                    id = to.id,
//                    name = to.name,
//                    routeInfoId = routeinfoget.id
//                });

//                context.RouteInfo.Add(new RouteInfo
//                {
//                    id = routeinfoget.id,
//                    distance = routeinfoget.distance,
//                    routeId = route.id,
//                    fromId = from.id,
//                    toId = to.id
//                });

//                foreach (ProviderGet provider in route.providers)
//                {   
//                    CompanyGet companyget = provider.company;
//                    context.Provider.Add(new Provider
//                    {
//                        id = provider.id,
//                        price = provider.price,
//                        flightStart = DateTime.Parse(provider.flightStart),
//                        flightEnd = DateTime.Parse(provider.flightEnd),
//                        companyId = companyget.id
//                    });

//                    context.Company.Add(new Company
//                    {
//                        id = companyget.id,
//                        name = companyget.name
//                    });
//                }
//            }
            
//            context.SaveChanges();
//        }
//    }



//    class ScheduleGet
//    {
//        public string? id { get; set; }
//        public string? validUntil { get; set; }
//        public RouteGet[]? legs { get; set; }
//    }

//    class RouteGet
//    {
//        public string? id { get; set; }
//        public RouteInfoGet? routeinfo { get; set; }
//        public ProviderGet[]? providers { get; set; }
//    }

//    class RouteInfoGet
//    {
//        public string? id { get; set; }
//        public PlaceGet? from { get; set; }
//        public PlaceGet? to { get; set; }
//        public long? distance { get; set; }
//    }
//    class PlaceGet
//    {
//        public string? id { get; set; }
//        public string? name { get; set; }
//    }

//    class ProviderGet
//    {
//        public string? id { get; set; }
//        public CompanyGet? company { get; set; }
//        public double? price { get; set; }
//        public string? flightStart { get; set; }
//        public string? flightEnd { get; set; }

//    }

//    class CompanyGet
//    {
//        public string? id { get; set; }
//        public string? name { get; set; }
//    }
//}