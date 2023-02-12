using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Item : MonoBehaviour
{
    public class ToolAction : UnityEvent{};
    public bool HoldAction = false;
    public bool Consumed = false;
    public GameObject InstantiatingModel;
    public PlayerActions M_PlayerActions;
    public PlayerAnimations M_PlayerAnimations;
    public PlayerInventory M_PlayerInventory;
    public int thisItemSlot;
    [SerializeField] Tfunc ActionType;
    public ToolAction action = new ToolAction();

    public void Begin(){
        int a = 0;
        foreach(Tfunc t in Enum.GetValues(typeof(Tfunc))){
            if((ActionType & t)>0){
                action.AddListener(M_PlayerActions.Actions[a]);
            }
            a++;
        }
    }
    void Update(){
        if(action!=null){
            if(HoldAction&&Input.GetMouseButton(0)){
                if(InstantiatingModel!=null){
                    M_PlayerActions.InstantiateModel = InstantiatingModel;
                }
                action.Invoke();
            }
            else if(HoldAction==false&&Input.GetMouseButtonDown(0)){
                if(InstantiatingModel!=null){
                    M_PlayerActions.InstantiateModel = InstantiatingModel;
                }
                action.Invoke();
            }
        }
    }
}
[System.Serializable, System.Flags] public enum Tfunc{
    Tilling = 1<<0,
    Seeding = 1<<1
}

