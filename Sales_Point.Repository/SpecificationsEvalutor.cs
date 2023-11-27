using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales_Point.Core.Entities;
using Sales_Point.Core.Specifications.Specifications;

namespace Sales_Point.Repository
{
    internal static class SpecificationsEvalutor<T> where T : BaseEntity
    {

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> Spec)
        {
           
            var Query = inputQuery;

            if(Spec.Cirteria is not null)
            {
                Query = Query.Where(Spec.Cirteria);
            }

            Query = Spec.OrderBy is { } ? Query.OrderBy(Spec.OrderBy) :
                     (Spec.OrderByDesc is { } ? Query.OrderByDescending(Spec.OrderByDesc):Query) ;


            Query = Spec.IsPaginationEnable is true ? Query.Skip(Spec.Skip).Take(Spec.Take) : Query;

            Query = Spec.Includes.Aggregate(Query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));


            return Query;
        }


    }
}
