using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DTemplate.Common;
using DTemplate.Common.Caching;
using DTemplate.Common.GenericRepo;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class ProductService : IProductService
    {


        public ServiceResult<Product> Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<Product> Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        [CacheAttribute(DurationMinute = 5)]
        public ServiceResult<Product> Get(Expression<Func<Product, bool>> filter = null)
        {
            return new ServiceResult<Product>
            {
                Data = new Product
                {
                    Name = DateTime.Now.ToString("ddMMYY HH:mm")
                }
            };
        }

        public ServiceResult<List<Product>> GetList(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<Product> Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
