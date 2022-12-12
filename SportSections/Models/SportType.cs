using Microsoft.VisualBasic;
using System.Threading.Channels;

namespace SportSections.Models
{
    public class Sport
    {
        public int SportId { get; set; }

        public string Name { get; set; }

        public string SportType { get; set; }

        public int ChanceOfInjury { get; set; }
    }
}
