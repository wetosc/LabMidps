using System;
using System.Collections.Generic;

namespace LAB7.Models
{
    public partial class Hobbit
    {
        public Hobbit()
        {
            Elf = new HashSet<Elf>();
            Wizard = new HashSet<Wizard>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }

        public virtual ICollection<Elf> Elf { get; set; }
        public virtual ICollection<Wizard> Wizard { get; set; }
    }
}
