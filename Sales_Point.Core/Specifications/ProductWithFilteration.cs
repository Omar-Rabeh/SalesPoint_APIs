using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales_Point.Core.Entities;
using Sales_Point.Core.Specifications.Specifications;

namespace Sales_Point.Core.Specifications
{
    public class ProductWithFiltrationForCountAsync: BaseSpecifications<Product>
    {

        public ProductWithFiltrationForCountAsync(ProductSpecParams specParams)
            : base(p => (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId) &&
                         (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId)
                  )
        {
            
        }
    }
}
