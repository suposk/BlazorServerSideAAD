﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Entities
{
    public enum RecomendedAction
    {
        Unknown = 0,
        None = 1,
        Warning = 2,
        CloseApplication = 3
    }


    public class AppVersion
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

