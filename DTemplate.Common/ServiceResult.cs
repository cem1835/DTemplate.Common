using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common
{
    public class ServiceResult<T> : IServiceResult where T : class, new()
    {
        public ServiceResult(ServiceResultType serviceResultType) { ServiceResultType = serviceResultType; }

        public ServiceResult() { }

        public string Message { get; set; }
        public ServiceResultType ServiceResultType { get; set; }
        public int ExceptionCode { get; set; }
        public Exception Exception { get; set; } 
        public T Data { get; set; }
    }

    public enum ServiceResultType
    {
        Success = 1,
        Error,
        Warning,
        Unknown
    }
    public interface IServiceResult
    {

    }
}
