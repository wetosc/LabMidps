using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLLSpecial;
namespace MIDPS_Lab5
{
    public class MiddleEarth
    {
        public const string Ring = "Ring";
        public const string Wizard = "Wizard";
        public const string Elf = "Elf";
        public const string Orc = "Orc";
        public const string Hobbit = "Hobbit";
        public const string User = "User";
    }
    
    public class Model
    {
        public Controller myController { get; set; }

        public string currentPage { get; set; }
        public int selectedID { get; set; }

        private Dictionary<string, bool> shouldHideListDict, shouldUpdateRowsDict, shouldShowImageDict;
        private Dictionary<string, string> table1TitleDict, table2TitleDict;
        private Dictionary<string, AddNewModel> addNewConfigDict;

        public User currentUser { get; set; }

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
                [MiddleEarth.Hobbit] = false,
                [MiddleEarth.User] = true
            };
            shouldUpdateRowsDict = new Dictionary<string, bool>
            {
                [MiddleEarth.Ring] = true,
                [MiddleEarth.Wizard] = true,
                [MiddleEarth.Elf] = true,
                [MiddleEarth.Orc] = false,
                [MiddleEarth.Hobbit] = true,
                [MiddleEarth.User] = false
            };
            table1TitleDict = new Dictionary<string, string>
            {
                [MiddleEarth.Ring] = "Ring",
                [MiddleEarth.Wizard] = "Wizard",
                [MiddleEarth.Elf] = "Elf",
                [MiddleEarth.Orc] = "Orc",
                [MiddleEarth.Hobbit] = "Hobbit",
                [MiddleEarth.User] = "Users"
            };
            table2TitleDict = new Dictionary<string, string>
            {
                [MiddleEarth.Ring] = "Wizard",
                [MiddleEarth.Wizard] = "Ring",
                [MiddleEarth.Elf] = "",
                [MiddleEarth.Orc] = "",
                [MiddleEarth.Hobbit] = "Friends",
                [MiddleEarth.User] = ""
            };
            addNewConfigDict = new Dictionary<string, AddNewModel>
            {
                [MiddleEarth.Ring] = new AddNewRing(),
                [MiddleEarth.Wizard] = new AddNewWizard(),
                [MiddleEarth.Elf] = new AddNewElf(),
                [MiddleEarth.Orc] = new AddNewOrc(),
                [MiddleEarth.Hobbit] = new AddNewHobbit(),
                [MiddleEarth.User] = new AddNewUser()
            };
            shouldShowImageDict = new Dictionary<string, bool>
            {
                [MiddleEarth.Ring] = false,
                [MiddleEarth.Wizard] = false,
                [MiddleEarth.Elf] = true && shouldHideListDict[MiddleEarth.Elf],
                [MiddleEarth.Orc] = false,
                [MiddleEarth.Hobbit] = false,
                [MiddleEarth.User] = false
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
        public bool shouldShowImage()
        {
            return shouldShowImageDict[currentPage];
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
                case MiddleEarth.User: return typeof(DLLSpecial.User);
                default: return null;
            }
        }



    }
}
