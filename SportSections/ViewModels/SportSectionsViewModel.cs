using SportSections.Models;
using System.Collections.Generic;

namespace SportSections.ViewModels
{
    public class SportSectionsViewModel
    {
        public List<SportSection> SportSections { get; set; }

        public string SectionNameSearch { get; set; }

        public string SportNameSearch { get; set; }
    }
}
