using SportSections.Models;
using System.Collections.Generic;

namespace SportSections.ViewModels
{
    public class TrainersViewModel
    {
        public List<Trainer> Trainers { get; set; }

        public OrderByAlphabet SurnameOrder { get; set; }

        public string SurnameSearch { get; set; }

        public int MinimumSectionCount { get; set; }

        public int MaximumSectionCount { get; set; }
    }
}
