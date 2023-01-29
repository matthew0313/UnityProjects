using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Structure
{    
    [SerializeField] bool Split = false;
    [SerializeField] int CurrentExit = 0;
    public GameObject QueueItem = null;
    public bool JustEmptied = false;
    [SerializeField] bool ForcedInsert = false;
    void OnDestroy(){
        base.OnDestroy();
        if(QueueItem!=null) Destroy(QueueItem);
    }
    public override void TickUpdate(){
        JustEmptied = false;
        if(HeldItem!=null && Split == false){
            for(int i = 0 ; i < OutputDir.Length ; i++){
                if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure!=null && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().HeldItem==null){
                    if(ForcedInsert==false){for(int k = 0 ; k < Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputDir.Length ; k++){
                        if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputPos[k].x == Position.x && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputPos[k].y == Position.y){
                            if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>()==null) Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().HeldItem = HeldItem;
                            else if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem==null) Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem = HeldItem;
                            else break;
                            HeldItem.transform.position = Manager.Land[OutputPos[i].x, OutputPos[i].y].Object.transform.position;
                            HeldItem = null;
                            JustEmptied = true;
                            return;
                        }
                    }
                    }
                    else{
                        if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>()==null) Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().HeldItem = HeldItem;
                            else if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem==null) Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem = HeldItem;
                            else continue;
                            HeldItem.transform.position = Manager.Land[OutputPos[i].x, OutputPos[i].y].Object.transform.position;
                            HeldItem = null;
                            JustEmptied = true;
                            return;
                    }
                }
            }
        }
        else if(HeldItem != null && Split){
            if(Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure!=null && Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().HeldItem==null){
                if(ForcedInsert==false){for(int k = 0 ; k < Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().InputDir.Length ; k++){
                    if(Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().InputPos[k].x == Position.x && Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().InputPos[k].y == Position.y){
                        if(Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>()==null) Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().HeldItem = HeldItem;
                        else if(Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>().QueueItem==null) Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>().QueueItem = HeldItem;
                        else break;
                        HeldItem.transform.position = Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Object.transform.position;
                        HeldItem = null;
                        CurrentExit++;
                        if(CurrentExit >= OutputDir.Length) CurrentExit = 0;
                        JustEmptied = true;
                        return;
                    }
                }
                }
                else{
                    if(Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>()==null) Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().HeldItem = HeldItem;
                        else if(Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>().QueueItem==null) Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>().QueueItem = HeldItem;
                        HeldItem.transform.position = Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Object.transform.position;
                        HeldItem = null;
                        CurrentExit++;
                        if(CurrentExit >= OutputDir.Length) CurrentExit = 0;
                        JustEmptied = true;
                        return;
                }
            }
            else{
                CurrentExit++;
                if(CurrentExit >= OutputDir.Length) CurrentExit = 0;
            }
        }
    }
    public override void TickUpdate2(){
        if(HeldItem!=null && Split == false){
            for(int i = 0 ; i < OutputDir.Length ; i++){
                if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure!=null && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().HeldItem==null && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>() != null && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().JustEmptied==true && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem == null){
                    if(ForcedInsert==false){for(int k = 0 ; k < Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputDir.Length ; k++){
                        if(Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputPos[k].x == Position.x && Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Structure>().InputPos[k].y == Position.y){
                            Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem = HeldItem;
                            HeldItem.transform.position = Manager.Land[OutputPos[i].x, OutputPos[i].y].Object.transform.position;
                            HeldItem = null;
                            JustEmptied = false;
                            return;
                        }
                    }
                    }
                    else{
                        Manager.Land[OutputPos[i].x, OutputPos[i].y].Script.Structure.GetComponent<Conveyor>().QueueItem = HeldItem;
                            HeldItem.transform.position = Manager.Land[OutputPos[i].x, OutputPos[i].y].Object.transform.position;
                            HeldItem = null;
                            JustEmptied = false;
                            return;
                    }
                }
            }
        }
        else if(HeldItem != null && Split){
            if(Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure!=null && Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().HeldItem==null && Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>() != null && Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>().JustEmptied == true && Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>().QueueItem == null){
                if(ForcedInsert==false){for(int k = 0 ; k < Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().InputDir.Length ; k++){
                    if(Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().InputPos[k].x == Position.x && Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Structure>().InputPos[k].y == Position.y){
                        Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>().QueueItem = HeldItem;
                        HeldItem.transform.position = Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Object.transform.position;
                        HeldItem = null;
                        CurrentExit++;
                        if(CurrentExit >= OutputDir.Length) CurrentExit = 0;
                        JustEmptied = false;
                        return;
                    }
                }
                }
                else{
                    Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Script.Structure.GetComponent<Conveyor>().QueueItem = HeldItem;
                        HeldItem.transform.position = Manager.Land[OutputPos[CurrentExit].x, OutputPos[CurrentExit].y].Object.transform.position;
                        HeldItem = null;
                        CurrentExit++;
                        if(CurrentExit >= OutputDir.Length) CurrentExit = 0;
                        JustEmptied = false;
                        return;
                }
            }
        }
    }
    public override void TickUpdate3(){
        if(QueueItem!=null) HeldItem = QueueItem;
        QueueItem = null;
    }
}
