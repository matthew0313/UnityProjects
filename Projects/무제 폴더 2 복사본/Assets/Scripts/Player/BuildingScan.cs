using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingScan : MonoBehaviour
{
    public class ScannedEvent : UnityEvent<Transform>{};
    public ScannedEvent Scanned = new ScannedEvent();
    [SerializeField] float Reach;
    [SerializeField] LayerMask WhatToHit;
    public Transform Chosen = null;
    bool c = false;
    void Awake(){
        Scanned.AddListener((Transform a) => {
            a.GetComponent<Outline>().enabled = true;
            Chosen = a;
        });
    }
    void Update(){
        Ray h = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(h.origin, h.direction, out hit, Reach, WhatToHit)){
            Scanned.Invoke(hit.transform.parent.parent);
        }
        else{
            Chosen.parent.parent.GetComponent<Outline>().enabled = false;
            Chosen = null;
        }
    }
}
