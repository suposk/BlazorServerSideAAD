//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Microsoft.Identity.Web;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebApiNetCore3.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VersionInMemoryController : ControllerBase
//    {
//        private readonly ILogger<VersionInMemoryController> _logger;
//        private readonly ITokenAcquisition _tokenAcquisition;
//        private static Dictionary<int, AppVersionDto> _dic = new Dictionary<int, AppVersionDto> 
//        {
//            {20, new AppVersionDto { CreatedAt = DateTime.Now.AddDays(-30), Link = "www.sme.sk", VersionValue = 20}},
//            {21, new AppVersionDto { CreatedAt = DateTime.Now.AddDays(-1), Link = "www.google.com", VersionValue = 21 , Details = "Some more text"}},
//        };

//        //static ConcurrentBag<TodoItem> todoStore = new ConcurrentBag<TodoItem>();

//        public VersionInMemoryController(ILogger<VersionInMemoryController> logger, ITokenAcquisition tokenAcquisition)
//        {
//            _logger = logger;
//            _tokenAcquisition = tokenAcquisition;
//            //_httpClient = new HttpClient();
//        }

//        // GET: api/<VersionController>
//        [HttpGet]
//        public List<AppVersionDto> Get()
//        {
//            _logger.LogInformation(ApiLogEvents.GetAllItems, $"{nameof(Get)} Started");
//            return _dic.Values.ToList();
//        }

//        // GET api/<VersionController>/5
//        [HttpGet("{id}", Name = "GetVersion")]
//        public async Task<ActionResult<AppVersionDto>> GetVersion(int id)
//        {
//            _logger.LogInformation(ApiLogEvents.GetItem, $"{nameof(GetVersion)} Started");

//            if (_dic.TryGetValue(id, out AppVersionDto find))            
//                return find;
//            else
//            {
//                var max = _dic.Keys.Max();
//                if (_dic.TryGetValue(max, out AppVersionDto latest))
//                    return latest;
//            }
//            return null;
//        }

//        //// POST api/<VersionController>
//        //[HttpPost]
//        ////public void PostVersion([FromBody] VersionDto dto)
//        //public async Task<ActionResult<AppVersionDto>> PostVersion(AppVersionDto dto)
//        //{
//        //    if (dto == null)
//        //        return BadRequest();

//        //    AppVersionDto result = null;
//        //    try
//        //    {
//        //        _logger.LogInformation(ApiLogEvents.InsertItem, $"{nameof(PostVersion)} Started");
//        //        var max = _dic.Keys.Max();               

//        //        if (_dic.TryGetValue(max, out AppVersionDto latest))
//        //        {
//        //            var next = max + 1;
//        //            if (next != dto.VersionValue)
//        //                dto.VersionValue = next;
//        //            dto.CreatedAt = DateTime.Now;
//        //            _dic.Add(next, dto);
//        //            result = dto;
                    
//        //            return CreatedAtRoute(nameof(GetVersion),
//        //                new { id = next }, result);                   
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        _logger.LogError(ex, nameof(PostVersion), null);
//        //        throw;

//        //    }
//        //    return result;
//        //}

//        //[HttpDelete("{id}")]
//        //public async Task<ActionResult> DeleteVersion(int id)
//        //{
//        //    if (id < 1)
//        //        return BadRequest();

//        //    try
//        //    {
//        //        _logger.LogInformation(ApiLogEvents.DeleteItem, $"{nameof(DeleteVersion)} Started");

//        //        if (_dic.ContainsKey(id) == false)
//        //        {
//        //            _logger.LogWarning(ApiLogEvents.DeleteItemNotFound, $"{nameof(DeleteVersion)} not found");
//        //            return NotFound();
//        //        }

//        //        _dic.Remove(id);
//        //        return NoContent();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        _logger.LogError(ex, nameof(DeleteVersion), null);
//        //        throw;
//        //    }            
//        //}
//    }
//}
