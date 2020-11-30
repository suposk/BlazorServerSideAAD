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
    public class ApiLogEvents
    {
        public const int GetAllItems = 1001;
        public const int GetItem = 1002;
        public const int InsertItem = 1003;
        public const int UpdateItem = 1004;
        public const int DeleteItem = 1005;
        public const int TestItem = 3000;
        public const int GetItemNotFound = 4000;
        public const int UpdateItemNotFound = 4001;
        public const int DeleteItemNotFound = 1005;
    }


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
            _logger.LogInformation(ApiLogEvents.GetAllItems, $"{nameof(Get)} Started");
            return _dic.Values.ToList();
        }

        // GET api/<VersionController>/5
        [HttpGet("{id}", Name = "GetVersion")]
        public async Task<ActionResult<VersionDto>> GetVersion(int id)
        {
            _logger.LogInformation(ApiLogEvents.GetItem, $"{nameof(GetVersion)} Started");

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

            VersionDto result = null;
            try
            {
                _logger.LogInformation(ApiLogEvents.InsertItem, $"{nameof(PostVersion)} Started");
                var max = _dic.Keys.Max();               

                if (_dic.TryGetValue(max, out VersionDto latest))
                {
                    var next = max + 1;
                    if (next != dto.Version)
                        dto.Version = next;
                    dto.CreatedAt = DateTime.Now;
                    _dic.Add(next, dto);
                    result = dto;
                    
                    return CreatedAtRoute(nameof(GetVersion),
                        new { id = next }, result);                   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(PostVersion), null);
                throw;

            }
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVersion(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                _logger.LogInformation(ApiLogEvents.DeleteItem, $"{nameof(DeleteVersion)} Started");

                if (_dic.ContainsKey(id) == false)
                {
                    _logger.LogWarning(ApiLogEvents.DeleteItemNotFound, $"{nameof(DeleteVersion)} not found");
                    return NotFound();
                }

                _dic.Remove(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(DeleteVersion), null);
                throw;
            }            
        }
    }
}
