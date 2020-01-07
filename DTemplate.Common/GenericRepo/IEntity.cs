using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.GenericRepo
{
    public interface IEntity
    {
        bool IsDeleted { get; set; }
        DateTime? CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        int? CreatedUserId { get; set; }
        int? ModifiedUserId { get; set; }
    }
}
