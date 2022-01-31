﻿using CMS.Application.Extensions;
using CMS.Application.Models.VMs;
using CMS.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Application.Models.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate = DateTime.Now;
        public Status Status => Status.Active;

        [NotMapped]
        [FileExtension]// Sonuna Attribute eki eklenmez.
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
        public List<GetCategoryVM> Categories { get; set; }
    }
}
