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
        public string WorkoutDate {  get; set; } 

        // one workout can have many exercises
        public ICollection<Exercise> Exercises { get; set; }
    }

    public class WorkoutDto
    {
        public int WorkoutId { get; set; }
        public string ExerciseName { get; set; }
        public string WorkoutDate { get; set; }
    }
}