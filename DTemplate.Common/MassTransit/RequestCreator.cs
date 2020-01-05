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

        public async Task<TResponse> CreateRequest<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class
        {
            var uriAddress = new Uri("rabbitmq://localhost/request_service");

            IRequestClient<TRequest, TResponse> client = _busControl.CreateRequestClient<TRequest, TResponse>(uriAddress, TimeSpan.FromSeconds(5));

            var response = await client.Request(request);

            return response;
        }
    }
}
