using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DTemplate.Common.MassTransit
{
    public class RequestCreator : IRequestCreator
    {
        private readonly IBusControl _busControl;

        public RequestCreator(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task<TResponse> RequestResponse<TRequest, TResponse>(TRequest request, string exchange = "request_service") where TRequest : class where TResponse : class
        {
            var uriAddress = new Uri("rabbitmq://localhost/" + exchange);
            var timeout = TimeSpan.FromSeconds(5);

            var client = _busControl.CreateRequestClient<TRequest>(uriAddress, timeout);

            var response = await client.GetResponse<TResponse>(request);

            return response.Message;
        }
    }
}
