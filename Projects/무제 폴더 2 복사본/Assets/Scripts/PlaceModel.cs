using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceModel : MonoBehaviour
{
    public bool canBeBuilt = true;
    public bool InArea = false;
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Border"){
            canBeBuilt = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if(other.gameObject.tag == "InArea"){
            InArea = true;
        }
    }
    public void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Border"){
            if(InArea){
                canBeBuilt = true;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else if(other.gameObject.tag == "InArea"){
            InArea = false;
        }
    }
}
