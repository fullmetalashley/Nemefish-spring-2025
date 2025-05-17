using System;
using UnityEngine;

[Serializable]
public class Fish
{
    public string _fishName;
    public string _fishType;
    public int _damageDealt;
    public int _health;
    public Item _successItem;

    public string _behavior;    //TODO: Replace this with a set of speeds, and a set of positions, for when the fish scatters after a cast
}
