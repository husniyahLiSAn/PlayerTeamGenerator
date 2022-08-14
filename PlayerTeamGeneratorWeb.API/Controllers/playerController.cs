using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerTeamGeneratorWeb.API.Models;
using PlayerTeamGeneratorWeb.API.Repositories;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlayerTeamGeneratorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class playerController : ControllerBase
    {
        private readonly PlayerTeamRepository repo;

        public playerController(PlayerTeamRepository repository)
        {
            repo = repository;
        }

        // TODO: GET api/<playerController>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPlayer(string filter, string sort, int start, int pageLimit)
        {
            try
            {
                var result = await repo.GetDataListPlayers(filter, sort, start, pageLimit);
                return Ok(result);
            }
            catch (Exception e) { return StatusCode(500, "{ \"message\": \"" + e.Message + "\" }"); }
        }

        // TODO: POST api/<playerController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostPlayer(FormPlayer model)
        {
            try
            {
                var result = await repo.InsertPlayer(model);
                if (result > 0)
                {
                    if (model.playerSkills.Count > 0)
                    {
                        foreach (FormPlayerSkills skills in model.playerSkills)
                        {
                            var res = await repo.InsertPlayerSkill(result, skills);
                            if (res < 0)
                            {
                                return BadRequest("{ \"message\": \"Invalid value for skill: " + skills.skill + "\" }");
                            }
                        }
                    }
                    return Ok();
                }
                return BadRequest("{ \"message\": \"Invalid value for position: " + model.position + "\" }");
            }
            catch (Exception e) { return StatusCode(500, "{ \"message\": \"" + e.Message + "\" }"); }
        }

        // TODO: PUT api/<playerController>/5
        [HttpPut("{playerId}")]
        public async Task<IActionResult> UpdatePlayer(int playerId, FormPlayer model)
        {
            try
            {
                var result = await repo.UpdatePlayer(playerId, model);
                if (result > 0)
                {
                    if (model.playerSkills.Count > 0)
                    {
                        foreach (FormPlayerSkills skills in model.playerSkills)
                        {
                            var res = await repo.InsertPlayerSkill(result, skills);
                            if (res < 0)
                            {
                                return BadRequest("{ \"message\": \"Invalid value for skill: " + skills.skill + "\" }");
                            }
                        }
                    }
                    return Ok();
                }
                return BadRequest("{ \"message\": \"Invalid value for position: " + model.position + "\" }");
            }
            catch (Exception e) { return StatusCode(500, "{ \"message\": \"" + e.Message + "\" }"); }
        }

        // TODO: DELETE api/<playerController>/5
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{playerId}")]
        public async Task<IActionResult> DeletePlayer(int playerId)
        {
            if (this.HttpContext.Request.Headers["Authorization"].ToString().Equals("Bearer SkFabTZibXE1aE14ckpQUUxHc2dnQ2RzdlFRTTM2NFE2cGI4d3RQNjZmdEFITmdBQkE="))
            {
                try
                {
                    var result = await repo.DeletePlayer(playerId);
                    if (result > 0)
                    {
                        return Ok();
                    }
                    return BadRequest(result);
                }
                catch (Exception e) { return StatusCode(500, "{ \"message\": \"" + e.Message + "\" }"); }
            }
            return Unauthorized();
        }
    }
}
