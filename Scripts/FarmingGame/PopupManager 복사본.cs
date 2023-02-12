using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupManager : MonoBehaviour
{
    [SerializeField] Popup CurrentPopup;
    public void OpenPopup(Popup a, bool Forced){
        if(Forced==false){
            if(CurrentPopup==null){
                CurrentPopup = a;
                CurrentPopup.Object.SetActive(true);
                if(CurrentPopup.OpenEvent.EventExist){
                    CurrentPopup.Move(true);
                }
            }
        }
        else{
            if(CurrentPopup!=null){
                if(CurrentPopup.CloseEvent.EventExist){
                    CurrentPopup.Move(false);
                }
                CurrentPopup.Object.SetActive(false);
            }
            CurrentPopup = a;
            CurrentPopup.Object.SetActive(true);
            if(CurrentPopup.OpenEvent.EventExist){
                CurrentPopup.Move(true);
            }
        }
    }
    public void ClosePopup(){
        if(CurrentPopup==null) return;
        if(CurrentPopup.CloseEvent.EventExist == true){
            CurrentPopup.Move(false);
        }
        CurrentPopup.Object.SetActive(false);
        CurrentPopup = null;
    }
}
[System.Serializable] public class Popup{
    public GameObject Object;
    public MoveEvent OpenEvent;
    public MoveEvent CloseEvent;
    public void Move(bool a){
        if(a==false){
            Object.transform.DOMove(CloseEvent.MovePos, CloseEvent.MoveTime);
        }
        else{
            Object.transform.DOMove(OpenEvent.MovePos, OpenEvent.MoveTime);
        }
    }
}
[System.Serializable] public struct MoveEvent{
    public bool EventExist;
    public Vector3 MovePos;
    public float MoveTime;
}
