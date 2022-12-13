using SportSections.Models;
using System.Collections.Generic;

namespace SportSections.ViewModels
{
    public class SportsmansViewModel
    {
        public List<Sportsman> Sportsmans { get; set; }

        public OrderByAlphabet SurnameOrder { get; set; }

        public string SurnameSearch { get; set; }

        public int MinimumSectionCount { get; set; }

        public int MaximumSectionCount { get; set; }
    }
}
