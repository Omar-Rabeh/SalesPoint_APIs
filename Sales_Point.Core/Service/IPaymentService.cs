using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales_Point.Core.Entities;

namespace Sales_Point.Core.Service
{
    public interface IPaymentService
    {

        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);

    }
}
