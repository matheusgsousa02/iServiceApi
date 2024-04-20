﻿namespace iServiceRepositories.Repositories.Models.Request
{
    public class ServiceModel
    {
        public int EstablishmentProfileID { get; set; }
        public int ServiceCategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double EstimatedDuration { get; set; }
        public byte[]? Photo { get; set; }
    }
}
