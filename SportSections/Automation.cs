using Microsoft.EntityFrameworkCore;
using SportSections.Models;
using System.Linq;

namespace SportSections
{
    public class Automation : IAutomation
    {
        private readonly SportContext _db;

        public Automation(SportContext db)
        {
            _db = db;
        }

        public Trainer GetBestTrainerForSection(int sectionId)
        {
            var section = _db.SportSections.Include(c => c.Sport)
                                            .Include(c => c.Coachings)
                                            .ThenInclude(c => c.Trainer).First(c => c.SportSectionId == sectionId);

            var trainers = _db.Trainers.Include(c => c.Coachings.Where(c => c.SportSection.Sport.SportType == section.Sport.SportType ||
                                                                            c.SportSection.Sport.Name == section.Sport.Name))
                                        .ThenInclude(c => c.SportSection)
                                        .ThenInclude(c => c.Sport)
                                        .Where(c => c.Coachings.Count() != 0)
                                        .AsEnumerable()
                                        .Where(c => !c.Coachings.Exists(k => k.SportSectionId == sectionId))
                                        .ToList();

            if (trainers.Count == 0)
            {
                return null;
            }

            var minSectionCount = trainers.Select(c => c.Coachings.Count).Min();
            trainers = trainers.Where(c => c.Coachings.Count == minSectionCount).ToList();

            if(trainers.Count == 1)
            {
                return trainers[0];
            }

            var trainersWithName = trainers.Where(c => c.Coachings.Exists(f => f.SportSection.Sport.Name == section.Sport.Name)).ToList();

            if(trainersWithName.Count != 0)
            {
                return trainersWithName[0];
            }

            return trainers[0];
        }
    }
}
