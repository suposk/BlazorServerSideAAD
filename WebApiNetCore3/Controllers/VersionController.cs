﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    //[AutoValidateAntiforgeryToken]
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;
        private readonly IRepository<AppVersion> _repository;
        private readonly IVersionRepository _versionRepository;
        private readonly IMapper _mapper;

        public VersionController(ILogger<VersionController> logger, 
            IRepository<AppVersion> repository,
            IVersionRepository versionRepository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _versionRepository = versionRepository;
            _mapper = mapper;
        }

        // GET: api/<VersionController>
        [HttpGet]
        public async Task<ActionResult<List<AppVersionDto>>> Get()
        {           
            try
            {
                _logger.LogInformation(ApiLogEvents.GetAllItems, $"{nameof(Get)} Started");

                var all = await _repository.GetAllAsync();
                var result = _mapper.Map<List<AppVersionDto>>(all);
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }

        // GET api/<VersionController>/5
        [HttpGet("{version}", Name = "GetVersion")]
        public async Task<ActionResult<AppVersionDto>> GetVersion(string version)
        {
            try
            {
                _logger.LogInformation(ApiLogEvents.GetItem, $"{nameof(GetVersion)} Started");

                AppVersionDto result = null;
                var res = await _versionRepository.GetVersion(version);
                result = _mapper.Map<AppVersionDto>(res);
                return result;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
    }
}
