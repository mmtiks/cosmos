using Microsoft.CodeAnalysis.CSharp.Syntax;
using Cosmos.Data;

namespace Cosmos
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<CosmosDataContext>();
            context.Database.EnsureCreated();
            AddPriceList(context);
        }

        private static void AddPriceList(CosmosDataContext context)
        {
            var priceList = context.Pricelist.FirstOrDefault();
            if (priceList != null) return;

            context.Pricelist.Add(new PriceList
            {
                id = "2",
                validUntil = DateTime.UtcNow,
                dateAdded = DateTime.UtcNow
            });
            context.SaveChanges();
            Console.WriteLine("Done");
        }
    }
}
