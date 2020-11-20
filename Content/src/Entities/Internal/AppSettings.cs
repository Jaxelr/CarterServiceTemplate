﻿namespace CarterService.Entities
{
    /// <summary>
    /// This is obtained from the appsettings.json on Startup
    /// </summary>
    public class AppSettings
    {
        public CacheConfig Cache { get; set; }
        public RouteDefinition RouteDefinition { get; set; }
        public HealthDefinition HealthDefinition { get; set; }
        public string[] ServerUrls { get; set; }
    }
}
