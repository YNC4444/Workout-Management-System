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
        // why is my [key] underlined (error)?
        [Key]

        public int ExerciseId {  get; set; }
        public string ExerciseName { get; set; }

        public string ExerciseDescription { get; set; }

    }
}