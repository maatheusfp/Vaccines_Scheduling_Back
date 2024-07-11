using Newtonsoft.Json;
using System.Net;
using Vaccines_Scheduling.Utility.Exceptions;
using Vaccines_Scheduling.Utility.Messages;
using Vaccines_Scheduling.Utility.Responses;

namespace Vaccines_Scheduling.Webapi.Middleware
{
        public class ApiMiddleware : IMiddleware
        {
            public ApiMiddleware()
            {

            }

            public async Task InvokeAsync(HttpContext context, RequestDelegate next)
            {
                try
                {
                    await next.Invoke(context);
                }
                catch (Exception ex)
                {
                    await HandleException(context, ex);
                }
            }

            private async Task HandleException(HttpContext context, Exception ex)
            {
                var response = context.Response;

                response.ContentType = "application/json";

                var messages = new List<string>();

                switch (ex)
                {
                    case BusinessException:
                        messages.Add(ex.Message);
                        break;
                    default:
                        messages.Add(InfraMessages.UnexpectedError);
                        break;
                }

                await response.WriteAsync(JsonConvert.SerializeObject(new DefaultResponse(HttpStatusCode.InternalServerError, messages)));
            }
        }
}

