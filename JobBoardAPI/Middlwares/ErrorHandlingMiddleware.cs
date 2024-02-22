
using JobBoardAPI.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JobBoardAPI.Middlwares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
               await  next.Invoke(context);

            }catch(NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
               await context.Response.WriteAsync(notFound.Message);

            }catch(Exception exception)
            {
                context.Response.StatusCode = 500;
              await  context.Response.WriteAsync(exception.Message);
            }


        }
    }
}
