using Application.DTOs.CategoryDTOs;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EComerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get-All-Categories")]
        public async Task<ActionResult> GetAllCategoriesPaged(
            [FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetAllCategoriesPagedQuery(pageNumber,pageSize));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Category")]
        public async Task<ActionResult> AddCategory(string categoryName)
        {
            var response = await _mediator.Send(new AddCategoryCommand(categoryName));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Update-Category-Name")]
        public async Task<ActionResult> UpdateCategoryName(CategoryDTO dto)
        {
            var response = await _mediator.Send(new UpdateCategoryNameCommand(dto));
            return Ok(response);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("Delete-Category")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand(categoryId));
            return Ok(response);
        }

    }
}
