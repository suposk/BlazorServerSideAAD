﻿using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNetCore3
{
    public enum RecomendedAction
    {
        Unknown = 0, 
        None = 1, 
        Warning = 2, 
        CloseApplication = 3 
    }

    public class AppVersionDto 
    {
        public int Id { get; set; }

        public RecomendedAction RecomendedAction { get; set; }

        public int VersionValue { get; set; }

        public string VersionFull { get; set; }

        public string Link { get; set; }

        public string Details { get; set; }

        public string DetailsFormat { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ReleasedAt { get; set; }
    }

}
