using System;
using System.Collections;
public class WorldHandler
{
    private ArrayList things;
    public ArrayList rings;
    public ArrayList wizards;
    public ArrayList elfs;
    public ArrayList orcs;
    public ArrayList hobbits;

    public WorldHandler()
    {
        things = new ArrayList();

        rings = new ArrayList();
        things.Add(rings);
        wizards = new ArrayList();
        things.Add(wizards);
        elfs = new ArrayList();
        things.Add(elfs);
        orcs = new ArrayList();
        things.Add(orcs);
        hobbits = new ArrayList();
        things.Add(hobbits);
    }

    public void AddNewOject(object data)
    {
        if (data is Ring)
        {
            rings.Add(data);
        }
        else if (data is Wizard)
        {
            wizards.Add(data);
        }
        else if (data is Elf)
        {
            elfs.Add(data);
        }
        else if (data is Orc)
        {
            orcs.Add(data);
        }
        else if (data is Hobbit)
        {
            hobbits.Add(data);
        }
    }

    public void AddNewOjectAtIndex(object data, int index)
    {
        if (data is Ring)
        {
            rings.Insert(index, data);
        }
        else if (data is Wizard)
        {
            wizards.Insert(index, data);
        }
        else if (data is Elf)
        {
            elfs.Insert(index, data);
        }
        else if (data is Orc)
        {
            orcs.Insert(index, data);
        }
        else if (data is Hobbit)
        {
            hobbits.Insert(index, data);
        }
    }

    public void destroyObject(Type type, int index)
    {
        if (type == typeof(Ring))
        {
            rings.RemoveAt(index);
        }
        if (type == typeof(Wizard))
        {
            wizards.RemoveAt(index);
        }
        if (type == typeof(Elf))
        {
            elfs.RemoveAt(index);
        }
        if (type == typeof(Orc))
        {
            orcs.RemoveAt(index);
        }
        if (type == typeof(Hobbit))
        {
            hobbits.RemoveAt(index);
        }
    }

    public void destroyMultipleObjects(Type type, int from, int to)
    {
        if (type == typeof(Ring))
        {
            rings.RemoveRange(from, from - to);
        }
        if (type == typeof(Wizard))
        {
            wizards.RemoveRange(from, from - to);
        }
        if (type == typeof(Elf))
        {
            elfs.RemoveRange(from, from - to);
        }
        if (type == typeof(Orc))
        {
            orcs.RemoveRange(from, from - to);
        }
        if (type == typeof(Hobbit))
        {
            hobbits.RemoveRange(from, from - to);
        }
    }


    public void SortGeneric<T>()
    {
        if (typeof(T) == typeof(Ring))
        {
            rings.Sort();
        }
        if (typeof(T) == typeof(Wizard))
        {
            wizards.Sort();
        }
        if (typeof(T) == typeof(Elf))
        {
            elfs.Sort();
        }
        if (typeof(T) == typeof(Orc))
        {
            orcs.Sort();
        }
        if (typeof(T) == typeof(Hobbit))
        {
            hobbits.Sort();
        }
    }

    public void ShowGeneric<T>()
    {
        ArrayList current = new ArrayList();
        if (typeof(T) == typeof(Ring))
        {
            current = rings;
        }
        if (typeof(T) == typeof(Wizard))
        {
            current = wizards;
        }
        if (typeof(T) == typeof(Elf))
        {
            current = elfs;
        }
        if (typeof(T) == typeof(Orc))
        {
            current = orcs;
        }
        if (typeof(T) == typeof(Hobbit))
        {
            current = hobbits;
        }

        foreach (T obj in current)
        {
            Console.WriteLine(obj.ToString());
        }

    }
}
