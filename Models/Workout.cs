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

        // a workout only focuses on one muscle (group)
        // a muscle (group) can be the focus of many owrkouts 
        [ForeignKey("Muscle")]
        public int muscleId { get; set; }
        public virtual Muscle Muscle { get; set; }
        public string WorkoutDate {  get; set; } 

        // one workout can have many exercises
        public ICollection<Exercise> Exercises { get; set; }


    }

    public class WorkoutDto
    {
        public int WorkoutId { get; set; }
        public string WorkoutDate { get; set; }
        public int muscleId { get; set; }


    }
}