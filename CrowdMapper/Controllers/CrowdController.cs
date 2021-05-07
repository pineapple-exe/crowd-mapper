using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CrowdMapper.Domain;

namespace CrowdMapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrowdController : ControllerBase
    {
        private readonly CrowdMapperDomain _crowdMapperDomain;

        public CrowdController(CrowdMapperDomain crowdMapperDomain)
        {
            _crowdMapperDomain = crowdMapperDomain;
        }

        [HttpPost("enter")]
        public IActionResult Enter(string zone, DateTime timestamp)
        {
            _crowdMapperDomain.Enter(zone, timestamp);

            return Ok();
        }

        [HttpGet("exit")]
        public IActionResult Exit(string zone, DateTime timestamp)
        {
            _crowdMapperDomain.Exit(zone, timestamp);

            return Ok();
        }

        [HttpGet("list")]
        public List<ZoneOutputModel> List()
        {
            return _crowdMapperDomain.List();
        }

        [HttpDelete("{zone}")]
        public IActionResult Clear(string zone)
        {
            _crowdMapperDomain.ClearZone(zone);

            return Ok();
        }
    }
}
