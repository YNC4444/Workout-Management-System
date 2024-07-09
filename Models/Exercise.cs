using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjectn01681774.Models
{
    public class Exercise
    {
        // describe an exercise
        [Key]

        public int ExerciseId {  get; set; }
        public string ExerciseName { get; set; }
        public string ExerciseDescription { get; set; }
        public int ExerciseWeight { get; set; }

        // an exercise can only focus on one muscle (group)
        // a muscle (group) can be the focus of many exercises
        //[ForeignKey("Muscle")]
        //public virtual Muscle Muscle { get; set; }

        // one exercise can be in many workouts
        public ICollection<Workout> Workouts { get; set; }
    }
}