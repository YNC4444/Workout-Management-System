﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProjectn01681774.Models
{
    public class Muscle
    {
        [Key]
        public int MuscleId { get; set; }
        public string GroupName { get; set; }
    }
}