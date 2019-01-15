﻿namespace SecretSanta.Domain.Models
{
    public class Gift : Entity
    {
        public string Title { get; set; }
        public User User { get; set; }
        public int Importance { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
    }
}