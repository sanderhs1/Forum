﻿using System;

namespace Renting.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

    }
}
