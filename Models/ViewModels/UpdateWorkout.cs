using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectn01681774.Models.ViewModels
{
    public class UpdateWorkout
    {
        // this viewmodel is a class which stores information that needs to be presented to /Workout/Update/

        // existing workout information

        public WorkoutDto Workout { get; set; }

        // all exercises to choose from when updating this workout
        
        public IEnumerable<ExerciseDto> ExerciseOptions { get; set; }
    }
}