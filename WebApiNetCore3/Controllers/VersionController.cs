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
        private static Dictionary<int, VersionDto> _dic = new Dictionary<int, VersionDto> 
        {
            {20, new VersionDto { CreatedAt = DateTime.Now.AddDays(-30), Link = "www.sme.sk", Version = 20}},
            {21, new VersionDto { CreatedAt = DateTime.Now.AddDays(-1), Link = "www.google.com", Version = 21 , Details = "Some more text"}},
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
            return _dic.Values.ToList();
        }

        // GET api/<VersionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VersionDto>> Get(int id)
        {
            if (_dic.TryGetValue(id, out VersionDto find))            
                return find;
            else
            {
                var max = _dic.Keys.Max();
                if (_dic.TryGetValue(max, out VersionDto latest))
                    return latest;
            }
            return null;
        }

        // POST api/<VersionController>
        [HttpPost]
        //public void PostVersion([FromBody] VersionDto dto)
        public async Task<ActionResult<VersionDto>> PostVersion(VersionDto dto)
        {
            if (dto == null)
                return BadRequest();

            return null;
        }
    }
}
