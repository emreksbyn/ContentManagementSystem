using CMS.Domain.Entities.Interfaces;
using CMS.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace CMS.Domain.Entities.Concrete
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public string FullName { get; set; }
        public string ImagePath { get; set; } = "/images/users/default.jpg";
        
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
