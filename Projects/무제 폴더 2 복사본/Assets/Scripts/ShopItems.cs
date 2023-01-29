using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    [System.Serializable] public class q{
        [SerializeField] public List<Item> Item;
    }
    [SerializeField] public List<q> Category;
}
[System.Serializable] public struct Item{
    [SerializeField] public Building Build;
    public string Description;
    public string Name;
    public string Category;
    public int price;
    public string[] Tags;
}