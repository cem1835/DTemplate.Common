using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.GenericRepo
{
    public class DBContextGeneric
    {
        public DbContext Context { get; set; }

        public DBContextGeneric(DbContext context)
        {
            Context = context;
        }

        public TContext ConvertTo<TContext>() where TContext : DbContext
        {
            return (TContext)this.Context;
        }
    }
}
