using System.Collections.Generic;

namespace LAB7.Models
{
    public class Wizard
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public ICollection<WizardRing> WizardRings { get; set; }
    }
}