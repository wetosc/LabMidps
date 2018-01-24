using System.Collections.Generic;

namespace LAB7.Models
{
    public class WizardRing
    {
        
        public int WizardID { get; set; }
        public int RingID { get; set; }

        public Wizard Wizard { get; set; }
        public Ring Ring { get; set; }
    }
}