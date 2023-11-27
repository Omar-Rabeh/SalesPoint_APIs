using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales_Point.Core.Entities;

namespace Sales_Point.Core.Repository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id);

        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string id);


    }
}
