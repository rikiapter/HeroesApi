using HeroesServices.Models;
using HeroesServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HeroesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TrainerController : ControllerBase
    {
        ITrainerService _trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }



        [HttpPost("Register")]
        public ActionResult<bool> Register(TrainerReq trainerReq)
        {
            bool res = _trainerService.Register(trainerReq);
            return Ok(res);
        }

    }
}