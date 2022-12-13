using SportSections.Models;

namespace SportSections
{
    public interface IAutomation
    {
        Trainer GetBestTrainerForSection(int sectionId);
    }
}
