using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL;

namespace MIDPS_Lab2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Program instance = new Program();
            instance.onChoice(instance.textManager.showMenu());

            Console.ReadLine();
        }

        TextManager textManager;
        SqlHandler sqlHandler;

        public Program()
        {
            sqlHandler = new SqlHandler();
            textManager = new TextManager();
            sqlHandler.Connect();
        }


        void onChoice(int choice)
        {
            switch (choice)
            {
                case 0:
                    {
                        object data = textManager.addThing();
                        sqlHandler.Insert(data);
                        break;
                    }
                case 1:
                    {

                        break;
                    }
                case 2:
                    {

                        break;
                    }
                case 3:
                    {

                        break;
                    }
                case 4:
                    {

                        break;
                    }
                case 5:
                    {

                        break;
                    }
                case 6:
                    {
                        Environment.Exit(0);
                        break;
                    }

            }



        }








    }

    class TextManager
    {

        public int showMenu()
        {
            Console.WriteLine("        Menu:");
            Console.WriteLine("    0) Add element");
            Console.WriteLine("    1) Add elements at index");
            Console.WriteLine("    2) Remove element");
            Console.WriteLine("    3) Remove multiple elements");
            Console.WriteLine("    4) Sort elements");
            Console.WriteLine("    5) Display elements");
            Console.WriteLine("    6) Exit program");
            return Int32.Parse(Console.ReadKey(false).KeyChar.ToString());
        }

        public object addThing()
        {
            Console.WriteLine(" What object do you want to create ?");
            Console.WriteLine("    0) Add a ring");
            Console.WriteLine("    1) Register a wizard");
            Console.WriteLine("    2) Register an elf");
            Console.WriteLine("    3) Create an orc");
            Console.WriteLine("    4) Visit a hobbit");
            char x = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (x)
            {
                case '0':
                    {
                        Ring temp = new Ring();
                        Console.WriteLine("Ring material?");
                        temp.Material = Console.ReadLine();
                        Console.WriteLine("Ring name?");
                        temp.Name = Console.ReadLine();
                        return temp;
                    }
                case '1':
                    {
                        Wizard temp = new Wizard();
                        Console.WriteLine("Wizard name?");
                        temp.Name = Console.ReadLine();
                        Console.WriteLine("Wizard color?");
                        temp.Color = Console.ReadLine();
                        Console.WriteLine("Wizard id?");
                        temp.id = UInt32.Parse(Console.ReadLine());
                        return temp;
                    }
                case '2':
                    {
                        Elf temp = new Elf();
                        Console.WriteLine("Elf name?");
                        temp.Name = Console.ReadLine();
                        Console.WriteLine("Elf category?");
                        temp.Category = Console.ReadLine();
                        Console.WriteLine("Elf id?");
                        temp.id = UInt32.Parse(Console.ReadLine());
                        return temp;
                    }
                case '3':
                    {
                        Orc temp = new Orc();
                        Console.WriteLine("Orc master?");
                        temp.Master = Console.ReadLine();
                        Console.WriteLine("Orc id?");
                        temp.id = UInt32.Parse(Console.ReadLine());
                        Console.WriteLine("Orc power?");
                        temp.Power = float.Parse(Console.ReadLine());
                        return temp;
                    }
                case '4':
                    {
                        Hobbit temp = new Hobbit();
                        Console.WriteLine("Hobbit name?");
                        temp.Name = Console.ReadLine();
                        Console.WriteLine("Hobbit family?");
                        temp.Family = Console.ReadLine();
                        Console.WriteLine("Hobbit id?");
                        temp.id = UInt32.Parse(Console.ReadLine());
                        return temp;
                    }
                default:
                    return null;
            }
        }

        public Type deleteItem()
        {
            Console.WriteLine(" What do you want to destroy today?");
            Console.WriteLine("    0) A ring");
            Console.WriteLine("    1) A wizard");
            Console.WriteLine("    2) An elf");
            Console.WriteLine("    3) An orc");
            Console.WriteLine("    4) A hobbit");
            char x = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (x)
            {
                case '0': return typeof(Ring);
                case '1': return typeof(Wizard);
                case '2': return typeof(Elf);
                case '3': return typeof(Orc);
                case '4': return typeof(Hobbit);
                default: return null;
            }
        }

        public void sortItems()
        {
            Console.WriteLine(" What do you want to sort today?");
            Console.WriteLine("    0) Rings");
            Console.WriteLine("    1) Wizards");
            Console.WriteLine("    2) Elfs");
            Console.WriteLine("    3) Orcs");
            Console.WriteLine("    4) Hobbits");
            char x = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (x)
            {
                case '0': break;
                case '1': break;
                case '2': break;
                case '3': break;
                case '4': break;
            }
        }
        void showItems()
        {
            Console.WriteLine(" What do you want to see today?");
            Console.WriteLine("    0) Rings");
            Console.WriteLine("    1) Wizards");
            Console.WriteLine("    2) Elfs");
            Console.WriteLine("    3) Orcs");
            Console.WriteLine("    4) Hobbits");
            char x = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (x)
            {
                case '0': break;
                case '1': break;
                case '2': break;
                case '3': break;
                case '4': break;
            }
        }
    }
}
