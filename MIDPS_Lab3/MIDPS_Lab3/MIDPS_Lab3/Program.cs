﻿using System;
using System.Collections.Generic;
using DLLSpecial;
using SQL;

namespace MIDPS_Lab3
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Disclaimer: ");
            Console.WriteLine("This program was created in educational purposes only. It doesn't work like an enterprise solution.");
            Console.WriteLine("That's why if you enter a wrong value you won't see some fancy error message;");
            Console.WriteLine("The program will continue to work, even if you try to break him.");
            Console.WriteLine();
            Program instance = new Program();
            while (true)
            {
                instance.onChoice(instance.textManager.showMenu());
                Console.ReadLine();
            }
        }

        TextManager textManager;

        public Program()
        {

            Singleton.Instance.start();
            textManager = new TextManager();
        }


        void onChoice(int choice)
        {

            switch (choice)
            {
                case 0:
                    {
                        SQLObject data = textManager.addThing();
                        if (data != null) Console.WriteLine(Singleton.Instance.Insert(data) > 0 ? "Succes" : "Error");
                        break;
                    }
                case 1:
                    {
                        Type t = textManager.deleteItem();
                        if (t != null) Console.WriteLine(Singleton.Instance.deleteOne(t) > 0 ? "Succes" : "Error");
                        break;
                    }
                case 2:
                    {
                        Type t = textManager.deleteItem();
                        if (t != null)
                        {
                            Console.WriteLine("How many records do you want to delete?");
                            UInt32 f = 0;
                            if (UInt32.TryParse(Console.ReadLine(), out f)) Console.WriteLine(Singleton.Instance.deleteMultiple(t, (int)f) > 0 ? "Succes" : "Error");
                        }
                        break;
                    }
                case 3:
                    {
                        Type t = textManager.updateData();
                        if (t != null)
                        {
                            Console.WriteLine("Write the old name of the object:");
                            string a = Console.ReadLine();
                            Console.WriteLine("Write the new name of the object");
                            string b = Console.ReadLine();
                            Console.WriteLine(Singleton.Instance.update(t, a, b) > 0 ? "Succes" : "Error");
                        }
                        break;
                    }
                case 4:
                    {
                        textManager.sortItems();
                        break;
                    }
                case 5:
                    {
                        textManager.showItems();
                        break;
                    }
                case 6:
                default:
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
            Console.WriteLine("    1) Remove element");
            Console.WriteLine("    2) Remove multiple elements");
            Console.WriteLine("    3) Rename character");
            Console.WriteLine("    4) Sort elements");
            Console.WriteLine("    5) Display elements");
            Console.WriteLine("    6) Exit program");
            int f = -1;
            if (Int32.TryParse(Console.ReadKey(false).KeyChar.ToString(), out f) && f >= 0 && f < 7)
            {
                return f;
            }
            else
            {
                return showMenu();
            }
        }

        public SQLObject addThing()
        {
            Console.WriteLine(" What object do you want to create ?");
            Console.WriteLine("    0) Add a ring");
            Console.WriteLine("    1) Register a wizard");
            Console.WriteLine("    2) Register an elf");
            Console.WriteLine("    3) Create an orc");
            Console.WriteLine("    4) Visit a hobbit");
            char x = Console.ReadKey().KeyChar;
            Console.WriteLine();
            UInt32 f;
            switch (x)
            {
                case '0':
                    {
                        Ring temp = new Ring();
                        Console.WriteLine("Ring material?");
                        temp.Material = Console.ReadLine();
                        Console.WriteLine("Ring name?");
                        temp.Name = Console.ReadLine();
                        Console.WriteLine("Write the list of ids of the ring masters:");
                        Console.WriteLine("Wrong ids will be discarded. Enter only verifyed ids. To end the list, type 0.");
                        temp.OwnerID = new List<uint>();
                        while (UInt32.TryParse(Console.ReadLine(), out f) && f > 0)
                        {
                            temp.OwnerID.Add((UInt32)f);
                        }
                        return temp;
                    }
                case '1':
                    {
                        Wizard temp = new Wizard();
                        Console.WriteLine("Wizard name?");
                        temp.Name = Console.ReadLine();
                        Console.WriteLine("Wizard color?");
                        temp.Color = Console.ReadLine();
                        Console.WriteLine("Write the list of ids of the ring owned by this wizard:");
                        Console.WriteLine("Wrong ids will be discarded. Enter only verifyed ids. To end the list, type 0.");
                        temp.RingID = new List<uint>();
                        while (UInt32.TryParse(Console.ReadLine(), out f) && f > 0)
                        {
                            temp.RingID.Add((UInt32)f);
                        }
                        return temp;
                    }
                case '2':
                    {
                        Elf temp = new Elf();
                        Console.WriteLine("Elf name?");
                        temp.Name = Console.ReadLine();
                        Console.WriteLine("Elf category?");
                        temp.Category = Console.ReadLine();
                        Console.WriteLine("Elf friend ID?");
                        Console.WriteLine("Wrong ids will be discarded. Enter only verifyed ids. To end the list, type 0.");
                        if (UInt32.TryParse(Console.ReadLine(), out f))
                        {
                            temp.HobbitFriend = (UInt32)f;
                        }
                        return temp;
                    }
                case '3':
                    {
                        Orc temp = new Orc();
                        Console.WriteLine("Orc master id?");
                        temp.Master = Console.ReadLine();
                        Console.WriteLine("Orc power?");
                        float f1 = 0; float.TryParse(Console.ReadLine(), out f1);
                        temp.Power = f1;
                        return temp;
                    }
                case '4':
                    {
                        Hobbit temp = new Hobbit();
                        Console.WriteLine("Hobbit name?");
                        temp.Name = Console.ReadLine();
                        Console.WriteLine("Hobbit family?");
                        temp.Family = Console.ReadLine();
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
                case '0': Singleton.Instance.sort<Ring>(); break;
                case '1': Singleton.Instance.sort<Wizard>(); break;
                case '2': Singleton.Instance.sort<Elf>(); break;
                case '3': Singleton.Instance.sort<Orc>(); break;
                case '4': Singleton.Instance.sort<Hobbit>(); break;
            }
        }
        public void showItems()
        {
            UInt32 f;
            Console.WriteLine(" What do you want to see today?");
            Console.WriteLine("    0) Rings");
            Console.WriteLine("    1) Wizards");
            Console.WriteLine("    2) Elfs");
            Console.WriteLine("    3) Orcs");
            Console.WriteLine("    4) Hobbits");
            Console.WriteLine("    5) Rings for master");
            Console.WriteLine("    6) Masters for ring");
            Console.WriteLine("    7) Friends for Hobbit");
            char x = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (x)
            {
                case '0': Singleton.Instance.read<Ring>(); break;
                case '1': Singleton.Instance.read<Wizard>(); break;
                case '2': Singleton.Instance.read<Elf>(); break;
                case '3': Singleton.Instance.read<Orc>(); break;
                case '4': Singleton.Instance.read<Hobbit>(); break;
                case '5':
                    {
                        Console.WriteLine("Write the id of the master:");
                        if (UInt32.TryParse(Console.ReadLine(), out f)) { Singleton.Instance.readSpecial(0, (int)f); }
                        break;
                    }
                case '6':
                    {
                        Console.WriteLine("Write the id of the ring:");
                        if (UInt32.TryParse(Console.ReadLine(), out f)) { Singleton.Instance.readSpecial(1, (int)f); }
                        break;
                    }
                case '7':
                    {
                        Console.WriteLine("Write the id of the hobbit:");
                        if (UInt32.TryParse(Console.ReadLine(), out f)) { Singleton.Instance.readSpecial(2, (int)f); }
                        break;
                    }
            }
        }
        public Type updateData()
        {
            Console.WriteLine(" What do you want to rename today?");
            Console.WriteLine("    0) Rings");
            Console.WriteLine("    1) Wizards");
            Console.WriteLine("    2) Elfs");
            Console.WriteLine("    3) Hobbits");
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

    }
}

