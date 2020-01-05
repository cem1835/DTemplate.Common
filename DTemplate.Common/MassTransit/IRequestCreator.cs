using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DTemplate.Common.MassTransit
{
    public interface IRequestCreator
    {
        Task<TResponse> CreateRequest<TRequest, TResponse>(TRequest request) where TRequest : class where TResponse : class;
    }
}
