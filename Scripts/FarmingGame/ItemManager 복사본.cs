using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public ItemInfo[] Items;
}
[System.Serializable] public struct ItemInfo{
    [Header("Basic Info")]public string ItemName;
    public int MaxStack;
    public Sprite Icon;
    [Header("Equipment")][Space(6)]public bool Equippable;
    public GameObject Model;
    [Header("Food Related")][Space(6)]public bool Edible;
    public float RestoreAmount;
}
