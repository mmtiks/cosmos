namespace Cosmos.ViewModels

{
    public class ProviderView
    {
        public string id { get; set; }
        public double price { get; set; }
        public DateTime flightStart { get; set; }

        public DateTime flightEnd { get; set; }
        public string companyId { get; set; }
        public string routeId { get; set; }
    }
}