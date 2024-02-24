using System;
using System.Collections.Generic;
using UnityEngine;
public class Identificator : ScriptableObject, IComparer<Identificator>, IComparable<Identificator>
{
    public string Id;

    public int Compare(Identificator x, Identificator y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return string.Compare(x.Id, y.Id, StringComparison.InvariantCultureIgnoreCase);
    }

    public int CompareTo(Identificator other)
    {
        if (other == null) return 1;

        return string.Compare(Id, other.Id, StringComparison.InvariantCultureIgnoreCase);
    }
    public static implicit operator string(Identificator identificator)
    {
        return identificator.Id;
    }
}
