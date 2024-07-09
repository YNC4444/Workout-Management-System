using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProjectn01681774.Models
{
    public class WorkoutXExercise
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Workout")]
        public int WorkoutId { get; set; }
        public virtual Workout Workout { get; set; }

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
    }
}