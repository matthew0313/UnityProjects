using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool canSwitchEquipment = true;
    [SerializeField] Popup InventoryPopup;
    public Slot[] Slots = new Slot[40];
    [SerializeField] Transform EquippedItemLocator;
    [SerializeField] PlayerActions M_PlayerActions;
    [SerializeField] PlayerAnimations M_PlayerAnimations;
    public GameObject Equipped;
    int EquipSlot;
    public int CurrentSlot = -1;
    bool chainEquip;
    void Update(){
        for(int i = 0 ; i < 10 ; i++){
            if(Input.GetKeyDown((KeyCode)(i+48)) && Slots[i+30].Info.Equippable && canSwitchEquipment && Slots[i+30].Amount>0){
                EquipSlot = i+30;
                if(Equipped!=null || CurrentSlot == EquipSlot){
                    M_PlayerAnimations.AnimationEquippedState(false);
                    if(CurrentSlot!=EquipSlot) chainEquip = true;
                }
                else M_PlayerAnimations.AnimationEquippedState(true);
            }
        }
    }
    public void Equip(){
        CurrentSlot = EquipSlot;
        Equipped = Instantiate(Slots[EquipSlot].Info.Model);
        Item item = Equipped.GetComponent<Item>();
        item.thisItemSlot = CurrentSlot;
        item.M_PlayerInventory = this;
        item.M_PlayerActions = M_PlayerActions;
        item.M_PlayerAnimations = M_PlayerAnimations;
        Equipped.transform.position = EquippedItemLocator.position;
        Equipped.transform.rotation = EquippedItemLocator.rotation;
        Equipped.transform.SetParent(EquippedItemLocator);
        item.Begin();
    }
    public void Unequip(){
        CurrentSlot = -1;
        Destroy(Equipped);
        if(chainEquip){
            M_PlayerAnimations.AnimationEquippedState(true);
            chainEquip = false;
        }
        else enableSwitch();
    }
    public void disableSwitch(){
        canSwitchEquipment = false;
    }
    public void enableSwitch(){
        canSwitchEquipment = true;
    }
     
}
[System.Serializable] public struct Slot{
    public ItemInfo Info;
    [Space(6)]public int Amount;
}
