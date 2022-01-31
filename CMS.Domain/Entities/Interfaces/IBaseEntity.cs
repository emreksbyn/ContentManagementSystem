using CMS.Domain.Enums;
using System;

namespace CMS.Domain.Entities.Interfaces
{
    public interface IBaseEntity
    {
        DateTime CreateDate { get; }
        DateTime? UpdateDate { get; }
        DateTime? DeleteDate { get; }
        Status Status { get; }
    }
}
