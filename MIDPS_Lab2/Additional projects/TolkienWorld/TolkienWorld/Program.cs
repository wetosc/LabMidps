using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDPS_Lab1
{
    class Program
    {
        public WorldHandler handler { get; set; }

        //public void Main(string[] args)
        //{
        //    Program instance = new Program();
        //    WorldHandler handler = new WorldHandler();
        //    while (true)
        //    {
        //        instance.showMenu();
        //        char x = Console.ReadKey().KeyChar;
        //        Console.WriteLine();

        //        switch (x)
        //        {
        //            case '0':
        //                handler.AddNewOject(instance.addThing());
        //                break;
        //            case '1':
        //                Console.WriteLine("At which index do you want to insert the element?");
        //                int insertIndex = Int32.Parse(Console.ReadLine());
        //                Console.WriteLine("How much elements do you want to add?");
        //                int varCount = Int32.Parse(Console.ReadLine());
        //                for (int i = 0; i < varCount; i++)
        //                {
        //                    handler.AddNewOjectAtIndex(instance.addThing(), insertIndex+i);
        //                }
        //                break;
        //            case '2':
        //                Console.WriteLine("At which index do you want to delete the element?");
        //                int removeIndex = Int32.Parse(Console.ReadLine());
        //                handler.destroyObject(instance.deleteItem(), removeIndex);
        //                break;
        //            case '3':
        //                Console.WriteLine("Write the start index from where to delete.");
        //                int startRemove = Int32.Parse(Console.ReadLine());
        //                Console.WriteLine("Write the end index until where to delete.");
        //                int endRemove = Int32.Parse(Console.ReadLine());
        //                handler.destroyMultipleObjects(instance.deleteItem(), startRemove, endRemove);
        //                break;
        //            case '4':
        //                instance.sortItems(handler);
        //                break;
        //            case '5':
        //                instance.showItems(handler);
        //                break;
        //            default:
        //                return;
        //        }
        //        //Console.Clear();
        //    }
        //}


        public void doThingsForNumber(int number)
        {
            switch (number)
            {
                case 0:
                    handler.AddNewOject(this.addThing());
                    break;
                case 1:
                    Console.WriteLine("At which index do you want to insert the element?");
                    int insertIndex = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("How much elements do you want to add?");
                    int varCount = Int32.Parse(Console.ReadLine());
                    for (int i = 0; i < varCount; i++)
                    {
                        handler.AddNewOjectAtIndex(this.addThing(), insertIndex + i);
                    }
                    break;
                case 2:
                    Console.WriteLine("At which index do you want to delete the element?");
                    int removeIndex = Int32.Parse(Console.ReadLine());
                    handler.destroyObject(this.deleteItem(), removeIndex);
                    break;
                case 3:
                    Console.WriteLine("Write the start index from where to delete.");
                    int startRemove = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Write the end index until where to delete.");
                    int endRemove = Int32.Parse(Console.ReadLine());
                    handler.destroyMultipleObjects(this.deleteItem(), startRemove, endRemove);
                    break;
                case 4:
                    this.sortItems(handler);
                    break;
                case 5:
                    this.showItems(handler);
                    break;
                default:
                    return;
            }
        }

            void showMenu()
        {
            Console.WriteLine("        Menu:");
            Console.WriteLine("    0) Add element");
            Console.WriteLine("    1) Add elements at index");
            Console.WriteLine("    2) Remove element");
            Console.WriteLine("    3) Remove multiple elements");
            Console.WriteLine("    4) Sort elements");
            Console.WriteLine("    5) Display elements");
            Console.WriteLine("    6) Exit program");
        }

        object addThing()
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
                        Console.WriteLine("Ring number?");
                        temp.id = UInt32.Parse(Console.ReadLine());
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

        Type deleteItem()
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

        void sortItems( WorldHandler handler)
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
                case '0': handler.SortGeneric<Ring>(); break;
                case '1': handler.SortGeneric<Wizard>(); break;
                case '2': handler.SortGeneric<Elf>(); break;
                case '3': handler.SortGeneric<Orc>(); break;
                case '4': handler.SortGeneric<Hobbit>(); break;
            }
        }
        void showItems(WorldHandler handler)
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
                case '0': handler.ShowGeneric<Ring>(); break;
                case '1': handler.ShowGeneric<Wizard>(); break;
                case '2': handler.ShowGeneric<Elf>(); break;
                case '3': handler.ShowGeneric<Orc>(); break;
                case '4': handler.ShowGeneric<Hobbit>(); break;
            }
        }
    }
}
