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
        private static Dictionary<int, AppVersionDto> _dic = new Dictionary<int, AppVersionDto> 
        {
            {20, new AppVersionDto { CreatedAt = DateTime.Now.AddDays(-30), Link = "www.sme.sk", VersionValue = 20}},
            {21, new AppVersionDto { CreatedAt = DateTime.Now.AddDays(-1), Link = "www.google.com", VersionValue = 21 , Details = "Some more text"}},
        };

        //static ConcurrentBag<TodoItem> todoStore = new ConcurrentBag<TodoItem>();

        public VersionController(ILogger<VersionController> logger)
        {
            _logger = logger;                        
        }

        // GET: api/<VersionController>
        [HttpGet]
        public List<AppVersionDto> Get()
        {
            _logger.LogInformation(ApiLogEvents.GetAllItems, $"{nameof(Get)} Started");
            return _dic.Values.ToList();
        }

        // GET api/<VersionController>/5
        [HttpGet("{id}", Name = "GetVersion")]
        public async Task<ActionResult<AppVersionDto>> GetVersion(int id)
        {
            _logger.LogInformation(ApiLogEvents.GetItem, $"{nameof(GetVersion)} Started");

            if (_dic.TryGetValue(id, out AppVersionDto find))            
                return find;
            else
            {
                var max = _dic.Keys.Max();
                if (_dic.TryGetValue(max, out AppVersionDto latest))
                    return latest;
            }
            return null;
        }
    }
}
