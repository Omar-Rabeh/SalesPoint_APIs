using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales_Point.Core;
using Sales_Point.Core.Entities;
using Sales_Point.Core.Specifications;
using SalesPoint.APIs.DTOs;
using SalesPoint.APIs.Errors;
using SalesPoint.APIs.Helpers;

namespace SalesPoint.APIs.Controllers
{
    public class ProductsController : APIBaceController
    {
        private readonly IUnitOfWork _unitOfWork;


        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }




        #region EndPoints For Product

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams Params)
        {

            #region Get All Async with Specification

            var Products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(new ProductWithBrandAndTypeSpecifications(Params));

            var DProdusts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);

            int Count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(new ProductWithFiltrationForCountAsync(Params));

            #endregion


            return Ok(new Pagination<ProductToReturnDto>(Params.PageIndex, Params.PageSize, Count, DProdusts));
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {

            #region Get All Async with Specification

            var product = await _unitOfWork.Repository<Product>().GetWithSpecAsync(new ProductWithBrandAndTypeSpecifications(p => p.Id == id));
            if (product is null)
                return NotFound(new ApiResponse(404));
            var DProduct = _mapper.Map<Product, ProductToReturnDto>(product);


            #endregion


            return Ok(DProduct);
        }

        #endregion


        #region Type 

        [HttpGet("Types")]
        public async Task<ActionResult<Pagination<ProductType>>> GetTypes()
        {
            var Types = await _unitOfWork.Repository<ProductType>().GetAllAsync();

            return Ok(Types);
        }
        #endregion


        #region Brand 
        [HttpGet("Brands")]
        public async Task<ActionResult<Pagination<ProductType>>> GetBrands()
        {
            var Brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

            return Ok(Brands);
        }
        #endregion






        #region Test Handel Request



        [HttpGet("NotFound")]

        public async Task<ActionResult<Product>> GetNotFound()
        {
            return Ok(NotFound(new ApiResponse(404)));

        }

        [HttpGet("InternalError")]

        public ActionResult NullError()
        {
            throw new NullReferenceException();
            return Ok();

        }

        #endregion

    }



}
