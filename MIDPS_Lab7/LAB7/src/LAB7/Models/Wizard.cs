using System;
using System.Collections.Generic;

namespace LAB7.Models
{
    public partial class Wizard
    {
        public Wizard()
        {
            Orc = new HashSet<Orc>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int? HobbitFriend { get; set; }

        public virtual ICollection<Orc> Orc { get; set; }
        public virtual Hobbit HobbitFriendNavigation { get; set; }
    }
}
