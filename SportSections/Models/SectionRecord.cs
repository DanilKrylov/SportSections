using System;

namespace SportSections.Models
{
    public class SectionRecord
    {
        public int SectionRecordId { get; set; }

        public int SportsmanId { get; set; }
        public Sportsman Sportsman { get; set; }

        public int SportSectionId { get; set; }
        public SportSection SportSection { get; set; }

        public DateTime RecordDate { get; set; }
    }
}
