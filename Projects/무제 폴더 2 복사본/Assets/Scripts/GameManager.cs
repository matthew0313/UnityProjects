using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Camera MainCam;

    public static GameManager instance{
        get{
            if(_instance==null){
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    public List<GameObject> ItemModels;
    [SerializeField] ItemName view;
    public List<string> Tags;
    public List<InventoryItem> Inventory;
    public int Money;
    public int Level;
    int _Experience;
    public int Experience{
        get{
            return _Experience;
        }
        set{
            _Experience = value;
            if(_Experience>=ExpRequired){
                _Experience -= ExpRequired;
                LevelUp();
            }
        }
    }
    public int ExpRequired;

    void LevelUp(){

    }
    public void ShopEnter(ShopScript a){
        MainCam.gameObject.Hide();
        a.enabled = true;
        a.ShopCamera.Show();
    }
    public void ShopExit(ShopScript a){
        MainCam.gameObject.Show();
        a.ShopCamera.Hide();
        a.enabled = false;
    }
    public void InventoryAdd(Item It, int amount){
        bool found = false;
        for(int i = 0 ; i < Inventory.Count ; i++){
            if(Inventory[i].Item.Name == It.Name){
                found = true;
                Inventory[i].Amount += amount;
                break;
            }
        }
        if(!found){
            InventoryItem a = new InventoryItem();
            a.Item = It;
            a.Amount = amount;
            Inventory.Add(a);
            for(int i = 0 ; i < It.Tags.Length ; i++){
                if(Tags.Find(x => x == It.Tags[i])==null){
                    Tags.Add(It.Tags[i]);
                }
            }
            Tags.Sort();
        }
    }
}
[System.Serializable] public class InventoryItem{
    public Item Item;
    public int Amount;
}