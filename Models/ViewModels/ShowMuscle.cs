﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectn01681774.Models.ViewModels
{
    public class ShowMuscle
    {
        public Muscle Muscle { get; set; }
        public IEnumerable<ExerciseDto> RelatedExercises { get; set; }
    }
}