using System.Collections.Generic;

namespace SportSections.Models
{
    public class Sportsman
    {
        public int SportsmanId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public List<SectionRecord> SectionRecords { get; set; }
    }
}
