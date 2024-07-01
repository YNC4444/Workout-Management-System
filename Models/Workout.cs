using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProjectn01681774.Models
{
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }

        // a workout can only have (for me) one muscle group (of focus) , but a muscleId can be in many workouts

        [ForeignKey("Muscle")]
        public int muscleId { get; set; }

        public virtual Muscle Muscle { get; set; }

        public string WorkoutDate {  get; set; } 


    }

    public class WorkoutDto
    {
        public int WorkoutId { get; set; }
        public string WorkoutDate { get; set; }
        public int muscleId { get; set; }


    }
}