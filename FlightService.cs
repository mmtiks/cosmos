using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace uptime.Services;

public class FlightService
{
    HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        FlightService program = new FlightService();
        await program.GetFlights();
    }

    private async Task GetFlights()
    {
        string response = await client.GetStringAsync("https://cosmos-odyssey.azurewebsites.net/api/v1.0/TravelPrices");
        Schedule schedule = JsonConvert.DeserializeObject<Schedule>(response);
        Console.WriteLine(schedule.id);
        Console.WriteLine(schedule.validUntil);
        Console.WriteLine();
        foreach (Route route in schedule.legs)
        {
            RouteInfo routeInfo = route.routeinfo;
            Console.WriteLine(routeInfo.from.name);
            Console.WriteLine(routeInfo.to.name);
            Console.WriteLine(routeInfo.distance);
            foreach (Provider provider in route.providers)
            {
                Console.WriteLine(provider.company.name);
                Console.WriteLine(provider.price);
            }
        }
    }
}

class Schedule
{
    public string? id { get; set; }
    public string? validUntil { get; set; }
    public Route[]? legs { get; set; }
}

class Route
{
    public string? id { get; set; }
    public RouteInfo? routeinfo { get; set; }
    public Provider[]? providers { get; set; }
}

class RouteInfo
{
    public string? id { get; set; }
    public Place? from { get; set; }
    public Place? to { get; set; }
    public long? distance { get; set; }
}
class Place
{
    public string? id { get; set; }
    public string? name { get; set; }
}

class Provider
{
    public string? id { get; set; }
    public Company? company { get; set; }
    public double? price { get; set; }
    public string? flightStart { get; set; }
    public string? flightEnd { get; set; }

}

class Company
{
    public string? id { get; set; }
    public string? name { get; set; }
}
