using System.Collections.Generic;

namespace LAB7.Models
{
    public class Ring
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }

        public ICollection<WizardRing> WizardRings { get; set; }
    }
}