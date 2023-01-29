using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInput : MonoBehaviour
{
    [SerializeField] BuildingStorage Storage;
    void OnTriggerEnter(Collider other){
        ItemName t = other.GetComponent<DroppedItem>().Type;
        if(other.tag == "Item" && t.HasFlag(Storage.AcceptedInput)){
            Storage.Input(t);
        }
    }
}
