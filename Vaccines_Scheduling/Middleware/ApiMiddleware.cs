using log4net;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using Vaccines_Scheduling.Repository.Interface;
using Vaccines_Scheduling.Utility.Attributes;
using Vaccines_Scheduling.Utility.Exceptions;
using Vaccines_Scheduling.Utility.Messages;
using Vaccines_Scheduling.Utility.Responses;

namespace Vaccines_Scheduling.Webapi.Middleware
{
    public class ApiMiddleware : IMiddleware
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ApiMiddleware));
        private readonly IMandatoryTransaction _mandatoryTransaction;
            
        public ApiMiddleware(IMandatoryTransaction mandatoryTransaction)
            {
                _mandatoryTransaction = mandatoryTransaction;
            }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var mandatoryTransaction = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<MandatoryTransactionsAttribute>();

            try
            {

                if (mandatoryTransaction != null)
                {
                    await _mandatoryTransaction.BeginTransactionAsync(mandatoryTransaction.IsolationLevel);

                    await next.Invoke(context);

                    await _mandatoryTransaction.CommitTransactionsAsync();
                }
                else
                {
                    await next.Invoke(context);
                }

                stopwatch.Stop();
                _log.InfoFormat("Service successfully executed: {0} {1} [{2} ms]", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                if (mandatoryTransaction != null)
                    await _mandatoryTransaction.RollbackTransactionsAsync();

                stopwatch.Stop();
                _log.Error($"Error in service: {context.Request.Path} / Message: {ex.Message} [{stopwatch.ElapsedMilliseconds}]", ex);
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

