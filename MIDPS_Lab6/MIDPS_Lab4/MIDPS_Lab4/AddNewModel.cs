using System;
using System.Collections.Generic;
using DLLSpecial;
using System.Linq;
namespace MIDPS_Lab5
{
    public abstract class AddNewModel
    {
        public Dictionary<string, string> viewDescription { get; set; } // fieldName : fieldType
        public AddNewModel()
        {
            viewDescription = new Dictionary<string, string>();
        }

        public abstract SQLObject buildObject(Dictionary<string, object> info);
    }

    public class AddNewRing : AddNewModel
    {
        public AddNewRing()
        {
            viewDescription = new Dictionary<string, string>
            {
                ["Name"] = "text",
                ["Material"] = "text",
                ["Owner"] = "list.multiple"
            };
        }
        override
        public SQLObject buildObject(Dictionary<string, object> info)
        {
            Ring temp = new Ring();
            temp.Name = (string)info["Name"];
            temp.Material = (string)info["Material"];
            List<string> ids = (info["Owner"] as string).Split(',').ToList<string>();
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
        public SQLObject buildObject(Dictionary<string, object> info)
        {
            Hobbit temp = new Hobbit();
            temp.Name = (string)info["Name"];
            temp.Region = (string)info["Region"];
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
                ["Friend"] = "list.single",
                ["Image"] = "image"
            };
        }
        override
        public SQLObject buildObject(Dictionary<string, object> info)
        {
            Elf temp = new Elf();
            try
            {
                temp.Name = (string)info["Name"];
                temp.Category = (string)info["Category"];
                uint nrtmp = 0;
                uint.TryParse((info["Friend"] as string).Trim(), out nrtmp);
                temp.HobbitFriend = nrtmp;
                temp.imageData = (byte[])info["Image"];
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("Image not found, but don't worry, that's not a problem.\n" + e.Message);
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
                ["Ring"] = "list.multiple"
            };
        }
        override
        public SQLObject buildObject(Dictionary<string, object> info)
        {
            Wizard temp = new Wizard();
            temp.Name = (string)info["Name"];
            temp.Color = (string)info["Color"];
            List<string> ids = (info["Ring"] as string).Split(',').ToList<string>();
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
        public SQLObject buildObject(Dictionary<string, object> info)
        {
            Orc temp = new Orc();
            float ptemp = 0;
            if (float.TryParse(info["Power"] as string, out ptemp))
            {
                temp.Power = ptemp;
            }
            return temp;
        }
    }

    public class AddNewUser : AddNewModel
    {
        public AddNewUser()
        {
            viewDescription = new Dictionary<string, string>
            {
                ["Name"] = "float",
                ["Password"] = "float",
                ["isAdmin"] = "checkbox",
                ["canViewExtra"] = "checkbox",
                ["canEdit"] = "checkbox",
            };
        }
        override
        public SQLObject buildObject(Dictionary<string, object> info)
        {
            User temp = new User();
            temp.name = info["Name"] as string;
            temp.password = info["Password"] as string;
            temp.isAdmin = (bool)info["isAdmin"];
            temp.canViewExtra = (bool)info["canViewExtra"];
            temp.canEdit = (bool)info["canEdit"];
            return temp;
        }
    }

}