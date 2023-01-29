using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemList ItemCode;
    public int SellPrice;
}
public enum ItemList{
    Coal,
    CopperOre,
    CopperIngot,
    CopperGear,
    CopperWire,
    IronOre,
    IronIngot,
    IronChain,
    IronPlate,
    CopperGearbox,
    ChainMechanism,
    PackedWater
    
}