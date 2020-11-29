using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNetCore3.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private static List<VersionDto> _list = new List<VersionDto> 
        { 
            new VersionDto { CreatedAt = DateTime.Now.AddDays(-30), Link = "www.sme.sk", Version = 20} ,
            new VersionDto { CreatedAt = DateTime.Now.AddDays(-1), Link = "www.google.com", Version = 21 , Details = "Some more text"} ,
        };

        //static ConcurrentBag<TodoItem> todoStore = new ConcurrentBag<TodoItem>();

        public VersionController(ILogger<VersionController> logger, ITokenAcquisition tokenAcquisition)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            //_httpClient = new HttpClient();
        }

        // GET: api/<VersionController>
        [HttpGet]
        public List<VersionDto> Get()
        {
            return _list;
        }

        // GET api/<VersionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VersionDto>> Get(int id)
        {
            var find = _list.FirstOrDefault(a => a.Version == id);
            if (find != null)
                return find;
            else
            {
                var m = _list.Max(a => a.Version);
                return _list.FirstOrDefault(a => a.Version == m);
            }
        }

        // POST api/<VersionController>
        [HttpPost]
        //public void PostVersion([FromBody] VersionDto dto)
        public async Task<ActionResult<VersionDto>> PostVersion(VersionDto dto)
        {
            return null;
        }
    }
}
