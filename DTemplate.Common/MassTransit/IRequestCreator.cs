using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DTemplate.Common.MassTransit
{
    public interface IRequestCreator
    {
        Task<TResponse> RequestResponse<TRequest, TResponse>(TRequest request, string exchange = "request_service") where TRequest : class where TResponse : class;
    }
}
