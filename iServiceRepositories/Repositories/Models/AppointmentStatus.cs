﻿namespace iServiceRepositories.Repositories.Models
{
    public class AppointmentStatus
    {
        public int AppointmentStatusId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
