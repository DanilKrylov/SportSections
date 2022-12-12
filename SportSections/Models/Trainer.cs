using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportSections.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [Range(3, 80, ErrorMessage = "Введіть значення від 3 до 80")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string WorkExperiance { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [Range(1000, 100_000, ErrorMessage = "Введіть значення від 1.000 до 100.000")]
        public int Salary { get; set; }

        public List<Coaching> Coachings { get; set; }
    }
}
