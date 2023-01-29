using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Seller : Structure
{
    [SerializeField] float Multiplier = 1.0f;
    [SerializeField] GameObject PriceIndicator;
    public override void TickUpdate(){
        if(HeldItem != null){
            Manager.Money += (int)((float)HeldItem.GetComponent<Item>().SellPrice*Multiplier);
            GameObject a = Instantiate(PriceIndicator);
            a.transform.position = transform.position;
            a.transform.rotation = Manager.NullIdentifier.transform.rotation;
            a.transform.GetChild(0).GetComponent<Text>().text = "+" + (int)((float)HeldItem.GetComponent<Item>().SellPrice*Multiplier) + "$"; 
            Manager.MoneyTextUpdate();
            Destroy(HeldItem);
        }
    }
}
