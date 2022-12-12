using System.Collections.Generic;

namespace SportSections.Models
{
    public class SportSection
    {
        public int SportSectionId { get; set; }

        public int Name { get; set; }

        public string Address { get; set; }

        public int Cost { get; set; }

        public string RecommendedTraining { get; set; }

        public string DoctorRecomended { get; set; }

        public int SportId { get; set; }

        public Sport Sport { get; set; }

        public List<Coaching> Coachings { get; set; }
        public List<SectionRecord> SectionRecords { get; set; }
    }
}
