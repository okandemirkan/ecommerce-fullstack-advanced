using Application.DTOs.ProductDTOs;
using Application.Enums;
using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EComerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get-All-Products")]
        public async Task<ActionResult> GetAllProductsPaged(
        [FromQuery] ProductSortType sortType = ProductSortType.Normal,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new GetAllProductsPagedQuery(sortType, pageNumber,
                pageSize));
            return Ok(response);
        }
        [HttpGet("Get-Products-By-Category/{id}")]
        public async Task<ActionResult> GetProductsByCategoryId(int id,
            [FromQuery] ProductSortType sortType = ProductSortType.Normal,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new GetProductsByCategoryIdQuery(sortType, id, pageNumber,
                pageSize));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-All-Soft-Deleted-Products")]
        public async Task<ActionResult> GetAllSoftDeletedProductsPaged(
            [FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetSoftDeletedProductsPagedQuery(pageNumber, pageSize));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Soft-Deleted-Product-By-Id/{productId}")]
        public async Task<ActionResult> GetSoftDeletedProductById(int productId)
        {
            var response = await _mediator.Send(new GetSoftDeletedProductByIdQuery(productId));
            return Ok(response);
        }

        [HttpGet("Get-Product-By-Id/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }

        [HttpGet("Search-Products-By-Name")]
        public async Task<ActionResult> SearchProductsByName(string productName,
            [FromQuery] ProductSortType sortType = ProductSortType.Normal,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new SearchProductsByNameQuery(false, sortType, productName, pageNumber,
                pageSize));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Search-Products-By-Name-As-Admin")]
        public async Task<ActionResult> SearchProductsByNameAsAdmin(string productName,
            [FromQuery] ProductSortType sortType = ProductSortType.Normal,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new SearchProductsByNameQuery(true, sortType, productName, pageNumber,
                pageSize));
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Product")]
        public async Task<ActionResult> AddProduct(AddProductCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = response }, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update-Product/{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductRequestDTO productDTO)
        {
            var response = await _mediator.Send(new UpdateProductCommand(id, productDTO));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Update-Product-Image")]
        public async Task<ActionResult> UpdateProductImage(int id, string imageUrl)
        {
            var response = await _mediator.Send(new UpdateProductImageCommand(id, imageUrl));
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Product")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var response = await _mediator.Send(new DeleteProductCommand(productId));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Soft-Delete-Product/{id}")]
        public async Task<ActionResult> SoftDeleteProduct(int id)
        {
            var response = await _mediator.Send(new SoftDeleteProductCommand(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Restore-Product/{productId}")]
        public async Task<ActionResult> RestoreProduct(int productId)
        {
            var response = await _mediator.Send(new RestoreDeletedProductCommand(productId));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Remove-Product-Image/{id}")]
        public async Task<ActionResult> RemoveProductImage(int id)
        {
            var response = await _mediator.Send(new RemoveProductImageCommand(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Add-Stock/{id}")]
        public async Task<ActionResult> AddStock(int id, int quantity)
        {
            var response = await _mediator.Send(new AddStockCommand(id, quantity));
            return Ok(response);
        }
    }
}
