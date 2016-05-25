using System;
using System.Collections.Generic;
using DLLSpecial;
using System.Linq;
namespace MIDPS_Lab4
{
    public abstract class AddNewModel
    {
        public Dictionary<string, string> viewDescription { get; set; } // fieldName : fieldType
        public AddNewModel()
        {
            viewDescription = new Dictionary<string, string>();
        }

        public abstract SQLObject buildObject(Dictionary<string, string> info);
    }

    public class AddNewRing : AddNewModel
    {
        public AddNewRing()
        {
            viewDescription = new Dictionary<string, string>
            {
                ["Name"] = "text",
                ["Material"] = "text",
                ["Owner"] = "list"
            };
        }
        override
        public SQLObject buildObject(Dictionary<string, string> info)
        {
            Ring temp = new Ring();
            temp.Name = info["Name"];
            temp.Material = info["Material"];
            List<string> ids = info["Owner"].Split(',').ToList<string>();
            List<uint> owners = new List<uint>();
            uint nrtmp = 0;
            foreach (string id in ids)
            {
                if (uint.TryParse(id.Trim(), out nrtmp))
                {
                    owners.Add(nrtmp);
                }
            }
            temp.OwnerID = owners;
            return temp;
        }
    }

    public class AddNewHobbit : AddNewModel
    {
        public AddNewHobbit()
        {
            viewDescription = new Dictionary<string, string>
            {
                ["Name"] = "text",
                ["Region"] = "text",
            };
        }
        override
        public SQLObject buildObject(Dictionary<string, string> info)
        {
            Hobbit temp = new Hobbit();
            temp.Name = info["Name"];
            temp.Region = info["Region"];
            return temp;
        }
    }

    public class AddNewElf : AddNewModel
    {
        public AddNewElf()
        {
            viewDescription = new Dictionary<string, string>
            {
                ["Name"] = "text",
                ["Category"] = "text",
                ["Friend"] = "number"
            };
        }
        override
        public SQLObject buildObject(Dictionary<string, string> info)
        {
            Elf temp = new Elf();
            temp.Name = info["Name"];
            temp.Category = info["Category"];
            uint nrtmp = 0;
            if (uint.TryParse(info["Friend"].Trim(), out nrtmp))
            {
                temp.HobbitFriend = nrtmp;
            }
            return temp;
        }
    }

    public class AddNewWizard : AddNewModel
    {
        public AddNewWizard()
        {
            viewDescription = new Dictionary<string, string>
            {
                ["Name"] = "text",
                ["Color"] = "text",
                ["Ring"] = "list"
            };
        }
        override
        public SQLObject buildObject(Dictionary<string, string> info)
        {
            Wizard temp = new Wizard();
            temp.Name = info["Name"];
            temp.Color = info["Color"];
            List<string> ids = info["Ring"].Split(',').ToList<string>();
            List<uint> rings = new List<uint>();
            uint nrtmp = 0;
            foreach (string id in ids)
            {
                if (uint.TryParse(id.Trim(), out nrtmp))
                {
                    rings.Add(nrtmp);
                }
            }
            temp.RingID = rings;
            return temp;
        }
    }

    public class AddNewOrc : AddNewModel
    {
        public AddNewOrc()
        {
            viewDescription = new Dictionary<string, string>
            {
                ["Power"] = "float"
            };
        }
        override
        public SQLObject buildObject(Dictionary<string, string> info)
        {
            Orc temp = new Orc();
            float ptemp = 0;
            if (float.TryParse(info["Power"], out ptemp))
            {
                temp.Power = ptemp;
            }
            return temp;
        }
    }

}