using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Services.Dto
{
    public class VersionDto
    {
        public int Version { get; set; }

        public string Link { get; set; }

        public string Details { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
