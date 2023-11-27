using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sales_Point.Core.Entities;
using Sales_Point.Core.Entities.OrderAggregate;

namespace Sales_Point.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {

            //Todo Seeding ProductBrand
            if (!context.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../Sales_Point.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count > 0)
                {
                    Brands.ForEach(async b => await context.Set<ProductBrand>().AddAsync(b));
                   // await context.SaveChangesAsync();
                }
            }

            //Todo Seeding ProductTypes
            if (!context.ProductTypes.Any())
            {

            var TypesData = File.ReadAllText("../Sales_Point.Repository/Data/DataSeed/types.json");

                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if (Types?.Count > 0)
                {
                    Types.ForEach(async t => await context.Set<ProductType>().AddAsync(t));
                   // await context.SaveChangesAsync();
                }
            }

            //Todo Seeding Product 
            if (!context.Products.Any())
            {
                // D:\Work\Personaly\Route\API\Demo\Sales_Point_Solution\Sales_Point.Repository\Data\DataSeed\products.json

                var ProductData = File.ReadAllText("../Sales_Point.Repository/Data/DataSeed/products.json");

                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (Products?.Count > 0)
                {
                    Products.ForEach(async p => await context.Set<Product>().AddAsync(p));
                   // await context.SaveChangesAsync();
                }
            }

            if (!context.DeliveryMethods.Any())
            {

                var DeliveryMethodsData = File.ReadAllText("../Sales_Point.Repository/Data/DataSeed/delivery.json");

                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);

                if (DeliveryMethods?.Count > 0)
                {
                    DeliveryMethods.ForEach(async dm => await context.Set<DeliveryMethod>().AddAsync(dm));
                }
            }



            await context.SaveChangesAsync();
        }

    }
}
