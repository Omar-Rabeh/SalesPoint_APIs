using Sales_Point.Core.Entities.OrderAggregate;
using Sales_Point.Core.Specifications.Specifications;

namespace Sales_Point.Core.Specifications.OrderSpec
{
    public class OrderSpecifications: BaseSpecifications<Order>
    {


       public OrderSpecifications(string Email):base(O=>O.BuyerEmail==Email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDesc(o => o.OrderDate);
        }

        public OrderSpecifications(string email, int orderId): base(o => o.BuyerEmail == email && o.Id == orderId) 
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }


    }
}
