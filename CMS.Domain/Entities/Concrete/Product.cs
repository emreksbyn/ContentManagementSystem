﻿using CMS.Domain.Entities.Interfaces;
using CMS.Domain.Enums;
using System;

namespace CMS.Domain.Entities.Concrete
{
    public class Product : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = "/images/products/default.jpg";

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
