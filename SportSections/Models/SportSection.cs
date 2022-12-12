using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportSections.Models
{
    public class SportSection
    {
        public int SportSectionId { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength =3, ErrorMessage ="Введіть данні довжиною від 3 до 25 символів")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [Range(1, 10000, ErrorMessage ="Введіть значення від 1 до 10000")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string RecommendedTraining { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string DoctorRecomended { get; set; }

        [Required(ErrorMessage ="Введіть данні в це поле")]
        public int SportId { get; set; }
        public Sport Sport { get; set; }

        public List<Coaching> Coachings { get; set; }
        public List<SectionRecord> SectionRecords { get; set; }
    }
}
