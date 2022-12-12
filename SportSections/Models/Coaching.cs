using System;

namespace SportSections.Models
{
    public class Coaching
    {
        public int CoachingId { get; set; }

        public int SportSectionId { get; set; }
        public SportSection SportSection { get; set; }

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }
    }
}
