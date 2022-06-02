using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HeroesDal.Models
{
    public   class Entity
    {
        [Key]
        public int GuidId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Ability { get; set; }
        public DateTime DateStart { get; set; }
        public string SuitColors { get; set; }
        public float StartingPower { get; set; }
        public float CurrentPower { get; set; }
        public int TrainerGuidId { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
