using HeroesServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesServices.Services
{
    public interface ITrainerService
    {
        bool Register(TrainerReq trainerReq);
    }
}
