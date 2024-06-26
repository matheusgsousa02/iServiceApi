﻿using Microsoft.AspNetCore.Http;

namespace iServiceRepositories.Repositories.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int EstablishmentUserProfileId { get; set; }
        public int ServiceCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int EstimatedDuration { get; set; }
        public string? ServiceImage { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public ServiceCategory? ServiceCategory { get; set; }
        public IFormFile? File { get; set; }
    }
}
