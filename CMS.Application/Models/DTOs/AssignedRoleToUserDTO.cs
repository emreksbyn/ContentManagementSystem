using CMS.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CMS.Application.Models.DTOs
{
    public class AssignedRoleToUserDTO
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> HasRole { get; set; }
        public IEnumerable<AppUser> HasNoRole { get; set; }
        public string RoleName { get; set; }
        // Kullanıcıya role atarken birden fazla kullanıcıyı bir role atarken yada o rolden alırken kullanıcıların id' lerini taşımak için alttaki arrayler kullanılacak.
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }
}
