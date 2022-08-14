using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerTeamGeneratorWeb.API.Models;
using PlayerTeamGeneratorWeb.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlayerTeamGeneratorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class teamController : ControllerBase
    {
        private readonly TeamRepository repo;

        public teamController(TeamRepository repository)
        {
            repo = repository;
        }

        // TODO: GET api/<teamController>
        [HttpGet]
        public async Task<IActionResult> GetTeam(string filter, string sort, int start, int pageLimit)
        {
            try
            {
                var result = await repo.GetMemberofTeam(filter, sort, start, pageLimit);
                return Ok(result);
            }
            catch (Exception e) { return StatusCode(500, "{ \"message\": \"" + e.Message + "\" }"); }
        }

        // TODO: POST api/<teamController>
        [HttpPost("process")]
        public async Task<IActionResult> AddTeam(List<AddTeam> model)
        {
            try
            {
                foreach (var item in model)
                {
                    if(repo.ValidationPosition(item.position).Result <= 0)
                        return BadRequest("{ \"message\": \"Insufficient number of players for position:  " + item.position + "\" }");
                    if(repo.ValidationSkill(item.mainSkill).Result <= 0)
                        return BadRequest("{ \"message\": \"Insufficient number of players for mainskill: " + item.mainSkill + "\" }");
                }

                var result = await repo.AddTeam(model);
                return Ok(result);
            }
            catch (Exception e) { return StatusCode(500, "{ \"message\": \"" + e.Message + "\" }"); }
        }
    }
}
