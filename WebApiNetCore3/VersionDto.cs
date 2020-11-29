using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNetCore3
{
    public class VersionDto 
    {
        public int Version { get; set; }

        public string Link { get; set; }

        public string Details { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
