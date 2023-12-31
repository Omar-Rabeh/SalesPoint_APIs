﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sales_Point.Core.Entities;
using Sales_Point.Core.Repository;
using SalesPoint.APIs.DTOs;
using SalesPoint.APIs.Errors;

namespace SalesPoint.APIs.Controllers
{

    public class BasketsController : APIBaceController
    {
        private readonly IBasketRepository _basketRep;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRep, IMapper mapper)
        {
            _basketRep = basketRep;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket = await _basketRep.GetBasketAsync(id);

            return basket ?? new CustomerBasket(id);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basketDto);
            var createOrUpdatebasket = await _basketRep.UpdateBasketAsync(basket);

            return createOrUpdatebasket is not null ? Ok(createOrUpdatebasket) : BadRequest(new ApiResponse(400, "there is problem in your Basket"));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        => await _basketRep.DeleteBasketAsync(id);


    }
}
