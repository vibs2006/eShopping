﻿using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            ProductResponse result = await _mediator.Send(query);
            return Ok(result);
        }

        //[HttpGet]
        //[Route("[action]/{productName}", Name = "GetProductByProductName")]
        //[ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IList<ProductResponse>>> GetProductByProductName(string productName)
        //{
        //    var query = new GetProductByNameQuery(productName);
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductByProductName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByProductName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts()
        {
            try
            {
                var query = new GetAllProductsQuery();
                var result = await _mediator.Send(query);                
                return Ok(result);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypesResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<TypesResponse>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{brand}", Name = "GetProductsByBrandName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductsByBrandName(string brand)
        {
            var query = new GetProductByBrandQuery(brand);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            ProductResponse result = await _mediator.Send(productCommand);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var query = new DeleteProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
