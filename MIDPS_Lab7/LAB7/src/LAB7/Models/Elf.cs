using System;
using System.Collections.Generic;

namespace LAB7.Models
{
    public partial class Elf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int? HobbitFriend { get; set; }
        public byte[] Image { get; set; }

        public virtual Hobbit HobbitFriendNavigation { get; set; }
    }
}
