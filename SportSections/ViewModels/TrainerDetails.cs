using SportSections.Models;
using System;
using System.Collections.Generic;

namespace SportSections.ViewModels
{
    public class TrainerDetails
    {
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public DateTime MinDateTime { get; set; }

        public DateTime MaxDateTime { get; set; } 
    }
}
