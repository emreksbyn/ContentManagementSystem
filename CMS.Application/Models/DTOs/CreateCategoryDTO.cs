using CMS.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Application.Models.DTOs
{
    public class CreateCategoryDTO
    {
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Sadece harf girilebilir")]
        public string Name { get; set; }
        public string Slug => Name.ToLower().Replace(" ", "_");
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
