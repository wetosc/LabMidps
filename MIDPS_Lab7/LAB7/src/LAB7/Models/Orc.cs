using System;
using System.Collections.Generic;

namespace LAB7.Models
{
    public partial class Orc
    {
        public int Id { get; set; }
        public double? Power { get; set; }
        public int? MasterId { get; set; }

        public virtual Wizard Master { get; set; }
    }
}
