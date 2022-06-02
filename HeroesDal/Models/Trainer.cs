using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HeroesDal.Models
{
   public class Trainer
    {
        [Key]
        public int TrainerGuidId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual List<Entity> Entities { get; set; }
    }


}
