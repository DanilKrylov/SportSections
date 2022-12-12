using System.Collections.Generic;

namespace SportSections.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public int WorkExperiance { get; set; }

        public int Address { get; set; }

        public int Salary { get; set; }

        public List<Coaching> Coachings { get; set; }
    }
}
