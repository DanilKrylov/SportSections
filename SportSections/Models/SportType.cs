using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace SportSections.Models
{
    public class Sport
    {
        public int SportId { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Введіть данні довжиною від 3 до 25 символів")]
        public string SportType { get; set; }

        [Required(ErrorMessage = "Введіть данні в це поле")]
        [Range(0, 100, ErrorMessage = "Введіть значення від 0 до 100")]
        public int ChanceOfInjury { get; set; }
    }
}
