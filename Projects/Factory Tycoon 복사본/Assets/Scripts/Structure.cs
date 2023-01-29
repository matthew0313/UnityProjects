using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public int Price;
    public GameManager Manager;
    public GameObject HeldItem = null;
    public int Rotation = 0;
    public Dir[] InputDir, OutputDir;
    public int OutputDistance = 0;
    public LandScript Land;
    public Cord Position;
    public Cord[] InputPos;
    public Cord[] OutputPos;
    public bool Use2ndTickUpdate = false;
    public bool Use3rdTickUpdate = false;
    public void OnDestroy(){
        if(HeldItem!=null) Destroy(HeldItem);
    }
    public void Start(){
        Position = Land.Coordinates;
        int Rot = 0;
        for(int i = 0 ; i < InputDir.Length ; i++){
            Rot = (int)InputDir[i]+Rotation;
            if(Rot>=4) Rot-=4;
                Cord a;
                a = Position;
                if(Rot==0) a.y += 1;
                else if(Rot==1) a.x += 1;
                else if(Rot==2) a.y -= 1;
                else if(Rot==3) a.x -= 1;
                InputPos[i] = a;
        }
        for(int i = 0 ; i < OutputDir.Length ; i++){
            Rot = (int)OutputDir[i]+Rotation;
            if(Rot>=4) Rot-=4;
                Cord a;
                a = Position;
                if(Rot==0) a.y += 1+OutputDistance;
                else if(Rot==1) a.x += 1+OutputDistance;
                else if(Rot==2) a.y -= 1+OutputDistance;
                else if(Rot==3) a.x -= 1+OutputDistance;
                OutputPos[i] = a;
        }
    }
    public virtual void TickUpdate(){
        
    }
    public virtual void TickUpdate2(){

    }
    public virtual void TickUpdate3(){
        
    }
}