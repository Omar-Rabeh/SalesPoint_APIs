﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sales_Point.Core.Entities;
using Sales_Point.Core.Repository;

namespace Sales_Point.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }



        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNull? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }


        public async Task<bool> DeleteBasketAsync(string id)
        => await _database.KeyDeleteAsync(id);



        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);

            var CreateOrUpdate = await _database.StringSetAsync(basket.Id, JsonBasket,TimeSpan.FromDays(1));
            if (!CreateOrUpdate) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
