using Application.Exceptions;
using Application.Result;
using Domain.Exceptions;
namespace EComerceWebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                await WriteResponse(context, 400,
                    string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            }
            catch(AuthException ex)
            {
                await WriteResponse(context, StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch(ForbiddenException ex)
            {
                await WriteResponse(context,StatusCodes.Status403Forbidden, ex.Message);
            }
            catch (NotFoundException ex)
            {
                await WriteResponse(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch(ArgumentException ex)
            {
                await WriteResponse(context,StatusCodes.Status400BadRequest, ex.Message);
            }
            catch(BadRequestException ex)
            {
                await WriteResponse(context,StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                await WriteResponse(context, StatusCodes.Status409Conflict, ex.Message);
            }
            catch (DomainException ex)
            {
                await WriteResponse(context, StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            catch (Exception)
            {
                await WriteResponse(context, StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
        }
        private async Task WriteResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var result = Result<object>.Failure(message);
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
