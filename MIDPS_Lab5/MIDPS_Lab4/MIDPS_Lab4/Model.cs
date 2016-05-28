using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDPS_Lab4
{
    public class MiddleEarth
    {
        public const string Ring = "Ring";
        public const string Wizard = "Wizard";
        public const string Elf = "Elf";
        public const string Orc = "Orc";
        public const string Hobbit = "Hobbit";
    }

    public class Model
    {
        public Controller myController { get; set; }

        public string currentPage { get; set; }
        public int selectedID { get; set; }

        private Dictionary<string, bool> shouldHideListDict, shouldUpdateRowsDict;
        private Dictionary<string, string> table1TitleDict, table2TitleDict;
        private Dictionary<string, AddNewModel> addNewConfigDict;

        public Model()
        {
            defineData();
        }
        private void defineData()
        {
            shouldHideListDict = new Dictionary<string, bool>
            {
                [MiddleEarth.Ring] = false,
                [MiddleEarth.Wizard] = false,
                [MiddleEarth.Elf] = true,
                [MiddleEarth.Orc] = true,
                [MiddleEarth.Hobbit] = false
            };
            shouldUpdateRowsDict = new Dictionary<string, bool>
            {
                [MiddleEarth.Ring] = true,
                [MiddleEarth.Wizard] = true,
                [MiddleEarth.Elf] = true,
                [MiddleEarth.Orc] = false,
                [MiddleEarth.Hobbit] = true
            };
            table1TitleDict = new Dictionary<string, string>
            {
                [MiddleEarth.Ring] = "Ring",
                [MiddleEarth.Wizard] = "Wizard",
                [MiddleEarth.Elf] = "Elf",
                [MiddleEarth.Orc] = "Orc",
                [MiddleEarth.Hobbit] = "Hobbit"
            };
            table2TitleDict = new Dictionary<string, string>
            {
                [MiddleEarth.Ring] = "Wizard",
                [MiddleEarth.Wizard] = "Ring",
                [MiddleEarth.Elf] = "",
                [MiddleEarth.Orc] = "",
                [MiddleEarth.Hobbit] = "Friends"
            };
            addNewConfigDict = new Dictionary<string, AddNewModel>
            {
                [MiddleEarth.Ring] = new AddNewRing(),
                [MiddleEarth.Wizard] = new AddNewWizard(),
                [MiddleEarth.Elf] = new AddNewElf(),
                [MiddleEarth.Orc] = new AddNewOrc(),
                [MiddleEarth.Hobbit] = new AddNewHobbit()
            };


        }
        public bool shouldHideList(string obj)
        {
            return shouldHideListDict[obj];
        }
        public bool shouldUpdateRows()
        {
            return shouldUpdateRowsDict[currentPage];
        }
        public string table1Title()
        {
            return table1TitleDict[currentPage];
        }
        public string table2Title()
        {
            return table2TitleDict[currentPage];
        }
        

        public AddNewModel addNewConfig()
        {
            return addNewConfigDict[currentPage];
        }


        public Type typeFromString(string str)
        {
            switch (str)
            {
                case MiddleEarth.Ring: return typeof(DLLSpecial.Ring);
                case MiddleEarth.Wizard: return typeof(DLLSpecial.Wizard);
                case MiddleEarth.Elf: return typeof(DLLSpecial.Elf);
                case MiddleEarth.Orc: return typeof(DLLSpecial.Orc);
                case MiddleEarth.Hobbit: return typeof(DLLSpecial.Hobbit);
                default: return null;
            }
        }



    }
}
