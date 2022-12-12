using System;
using System.ComponentModel.DataAnnotations;

namespace SportSections.Models
{
    public class SectionRecord
    {
        public int SectionRecordId { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        public int SportsmanId { get; set; }
        public Sportsman Sportsman { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        public int SportSectionId { get; set; }
        public SportSection SportSection { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        public DateTime RecordDate { get; set; }
    }
}
