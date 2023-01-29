using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField] ShopItems Items;
    [SerializeField] GameObject CategoryChooser;
    [SerializeField] GameObject ItemChooser;
    [SerializeField] GameObject DescPanel;
    [SerializeField] Text[] DescText;
    [SerializeField] Text ItemText;
    [SerializeField] GameObject ExamplePositioner;
    public GameObject ShopCamera;
    int CurrentCategory = -1;
    int CurrentItem = 0;
    bool DPOpen = false;

    void Update(){
        ExamplePositioner.transform.Rotate(0.0f, 0.5f, 0.0f);
    }
    public void CategoryChoose(int c){
        CurrentCategory = c;
        CategoryChooser.Hide();
        ItemChooser.Show();
        CurrentItem = 0;
        SetItem();
    }
    public void Exit(){
        if(CurrentCategory!=-1){
            CurrentCategory = -1;
            CategoryChooser.Show();
            ItemChooser.Hide();
        }
        else{
            GameManager.instance.ShopExit(this);
        }
    }
    public void NextItem(){
        if(CurrentItem+1<Items.Category[CurrentCategory].Item.Count){
            CurrentItem++;
            SetItem();
        }
    }
    public void PrevItem(){
        if(CurrentItem-1>=0){
            CurrentItem--;
            SetItem();
        }
    }
    public void DPToggle(){
        if(DPOpen) DescPanel.Hide();
        else DescPanel.Show();
        DPOpen = !DPOpen;
    }
    public void SetItem(){
        ItemText.text = Items.Category[CurrentCategory].Item[CurrentItem].Name + "\n" + Items.Category[CurrentCategory].Item[CurrentItem].price + "$";
        DescText[0].text = Items.Category[CurrentCategory].Item[CurrentItem].Name;
        DescText[1].text = Items.Category[CurrentCategory].Item[CurrentItem].Description;
        GameObject a = Instantiate(Items.Category[CurrentCategory].Item[CurrentItem].Build.Model, ExamplePositioner.transform.position, ExamplePositioner.transform.rotation);
        a.transform.SetParent(ExamplePositioner.transform);
    }
}
