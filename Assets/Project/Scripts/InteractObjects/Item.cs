using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    
    public enum ItemType {
        GarageKey,
        Screwdriver,
        Batteries,
        Stick,
        Fuse,
    }

    public ItemType itemType;
}
