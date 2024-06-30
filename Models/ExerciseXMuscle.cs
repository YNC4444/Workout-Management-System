using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProjectn01681774.Models
{
    public class ExerciseXMuscle
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        [ForeignKey("Muscle")]
        public int muscleId { get; set; }
        public virtual Muscle Muscle { get; set; }
    }
}