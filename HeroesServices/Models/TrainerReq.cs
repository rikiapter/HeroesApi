using HeroesDal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesServices.Models
{
    public class TrainerReq
    {
        public int TrainerGuidId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
    public static class ExtensionsTrainer
    {
        public static Trainer ToEntity(this TrainerReq request)
     => new Trainer
     {
         Name = request.Name,
         Password = request.Password,
         TrainerGuidId = request.TrainerGuidId
     };
    }
}
