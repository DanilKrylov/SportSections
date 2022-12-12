using System;
using System.ComponentModel.DataAnnotations;

namespace SportSections.Models
{
    public class Coaching
    {
        public int CoachingId { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        public int SportSectionId { get; set; }
        public SportSection SportSection { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        public DateTime FinishTime { get; set; }
    }
}
