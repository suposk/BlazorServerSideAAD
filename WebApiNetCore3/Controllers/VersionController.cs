using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Server.Entities;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiNetCore3.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;
        private readonly IRepository<AppVersion> _repository;
        

        public VersionController(ILogger<VersionController> logger, IRepository<AppVersion> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<VersionController>
        [HttpGet]
        public  List<AppVersionDto> Get()
        {
            _logger.LogInformation(ApiLogEvents.GetAllItems, $"{nameof(Get)} Started");
            var all = _repository.GetAll();
            return null;
        }

        // GET api/<VersionController>/5
        [HttpGet("{id}", Name = "GetVersion")]
        public async Task<ActionResult<AppVersionDto>> GetVersion(int id)
        {
            _logger.LogInformation(ApiLogEvents.GetItem, $"{nameof(GetVersion)} Started");
            var all = await _repository.GetAllAsync();

            //if (_dic.TryGetValue(id, out AppVersionDto find))            
            //    return find;
            //else
            //{
            //    var max = _dic.Keys.Max();
            //    if (_dic.TryGetValue(max, out AppVersionDto latest))
            //        return latest;
            //}
            return null;
        }
    }
}
