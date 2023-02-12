using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class StructureScript : MonoBehaviour
{
    public class StructureAction : UnityEvent{};
    [SerializeField] PlayerInventory M_PlayerInventory;
    [SerializeField] bool[,] OccupySpace;
    [SerializeField] int centerX, centerY;
    [SerializeField] bool function;
    [SerializeField] bool canInterior;
    [SerializeField] func runningFunction;
    public UnityAction[] Functions = new UnityAction[10];
    StructureAction action = new StructureAction();
    void Start(){
        //Functions[0] = WaterFill;
    }
    void Begin(){
        int a = 0;
        foreach(func t in Enum.GetValues(typeof(func))){
            if((runningFunction&t)>0){
                action.AddListener(Functions[a]);
            }
            a++;
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){

        }
    }
    public void Activate(){
        
    }
}
[System.Serializable, System.Flags] public enum func{
    WaterFilling = 1<<0
}

