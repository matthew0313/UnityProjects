using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickUpdate : MonoBehaviour
{
    [SerializeField] GameManager Manager;
    [SerializeField] int ConveyorSpeed;
    [SerializeField] int ConveyorSpeedTmp;
    void Start(){
        StartCoroutine(TUpdate());
    }

    IEnumerator TUpdate(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            for(int i = 0 ; i < Manager.BuiltObjects.Count ; i++){
                Manager.BuiltObjects[i].TickUpdate();
            }
            for(int i = 0 ; i < Manager.TickUpdate2Users.Count ; i++){
                Manager.TickUpdate2Users[i].TickUpdate2();
            }
            for(int i = 0 ; i < Manager.TickUpdate3Users.Count ; i++){
                Manager.TickUpdate3Users[i].TickUpdate3();
            }
        }
    }
}
