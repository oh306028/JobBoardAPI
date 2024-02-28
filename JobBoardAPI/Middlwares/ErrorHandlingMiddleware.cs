
using AutoMapper.Internal;
using JobBoardAPI.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JobBoardAPI.Middlwares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
               await  next.Invoke(context);

            }
            catch (ForbidedException forbid)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbid.Message);

                _logger.LogError(forbid.Message);

            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
               await context.Response.WriteAsync(notFound.Message);

                _logger.LogError(notFound.Message);

            }
            catch(BadRequestException badRequest)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);

                _logger.LogError(badRequest.Message);
            }
            catch (RegistrationException regEx)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(regEx.Message);


                _logger.LogError(regEx.Message);
            }
            catch (Exception exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");


                _logger.LogError(exception.Message);
            }


        }
    }
}
